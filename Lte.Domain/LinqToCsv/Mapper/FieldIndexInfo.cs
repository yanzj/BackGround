using Lte.Domain.LinqToCsv.Description;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.LinqToCsv.Mapper
{
    public class FieldIndexInfo
    {
        // IndexToInfo is used to quickly translate the index of a field
        // to its TypeFieldInfo.

        public TypeFieldInfo[] IndexToInfo { get; }

        /// <summary>
        /// Contains a mapping between the CSV column indexes that will read and the property indexes in the business object.
        /// </summary>
        private readonly IDictionary<int, int> _mappingIndexes;

        public FieldIndexInfo(Dictionary<string, TypeFieldInfo> nameToInfo)
        {
            var nbrTypeFields = nameToInfo.Keys.Count;
            IndexToInfo = new TypeFieldInfo[nbrTypeFields];
            _mappingIndexes = new Dictionary<int, int>();

            var i = 0;
            foreach (var kvp in nameToInfo)
            {
                IndexToInfo[i++] = kvp.Value;
            }

            // Sort by FieldIndex. Fields without FieldIndex will 
            // be sorted towards the back, because their FieldIndex
            // is Int32.MaxValue.
            //
            // The sort order is important when reading a file that 
            // doesn't have the field names in the first line, and when
            // writing a file. 
            //
            // Note that for reading from a file with field names in the 
            // first line, method ReadNames reworks IndexToInfo.
            Array.Sort(IndexToInfo);
        }

        public void AddMappingIndex(int i, int currentNameIndex)
        {
            _mappingIndexes.Add(i, currentNameIndex);
        }

        public void UpdateIndexToInfo<T>(IDataRow row, Dictionary<string, TypeFieldInfo> nameToInfo,
            bool enforceCsvColumnAttribute, string fileName)
        {
            for (var i = 0; i < row.Count; i++)
            {
                if (!_mappingIndexes.ContainsKey(i))
                {
                    continue;
                }

                IndexToInfo[_mappingIndexes[i]] = nameToInfo[row[i].Value];
                if (enforceCsvColumnAttribute && (!IndexToInfo[i].HasColumnAttribute))
                {
                    // enforcing column attr, but this field/prop has no column attr.
                    throw new MissingCsvColumnAttributeException(typeof(T).ToString(), row[i].Value, fileName);
                }
            }
        }

        public TypeFieldInfo QueryTypeFieldInfo(bool ignoreUnknownColumns, int i)
        {
            //If there is some index mapping generated and the IgnoreUnknownColums is `true`
            if (ignoreUnknownColumns && _mappingIndexes.Count > 0)
            {
                return !_mappingIndexes.ContainsKey(i) ? null : IndexToInfo[_mappingIndexes[i]];
            }
            return IndexToInfo[i];
        }

        public List<int> GetCharLengthList()
        {
            return IndexToInfo.Select(e => e.CharLength).ToList();
        }

        public int GetMaxRowCount(int defaultRowCount)
        {
            return _mappingIndexes.Count > 0 ? defaultRowCount : Math.Min(defaultRowCount, IndexToInfo.Length);
        }
    }
}
