using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Dt
{
    public static class FileRecordQuery
    {
        public static List<FileRecord2G> MergeRecords(this IEnumerable<FileRecord2GCsv> csvs)
        {
            return
                csvs.MergeRecordList()
                    .Select(list => list.MergeSubRecords((subList, lon, lat) => new FileRecord2G
                    {
                        Ecio = subList.Where(x => x.EcIo != null).Average(x => x.EcIo),
                        Longtitute = lon,
                        Lattitute = lat,
                        Pn = (short?) subList.FirstOrDefault(x => x.Pn != null)?.Pn,
                        RxAgc = subList.Where(x => x.RxAgc != null).Average(x => x.RxAgc),
                        TxAgc = subList.Where(x => x.TxAgc != null).Average(x => x.TxAgc),
                        StatTime = subList[0].StatTime,
                        TxGain = subList.Where(x => x.TxGain != null).Average(x => x.TxGain),
                        TxPower = subList.Where(x => x.TxPower != null).Average(x => x.TxPower),
                        RasterNum = (short)((short)((lat - 22.64409) / 0.00895) * 104 + (short)((lon - 112.387654) / 0.0098))
                    }, (csv, stat) =>
                    {
                        if (csv.RxAgc != null) stat.RxAgc = csv.RxAgc;
                        if (csv.EcIo != null) stat.Ecio = csv.EcIo;
                        return stat.RxAgc != null && stat.Ecio != null;
                    })).Aggregate((x, y) => x.Concat(y)).ToList();
        }

        public static List<FileRecord3G> MergeRecords(this IEnumerable<FileRecord3GCsv> csvs)
        {
            return
                csvs.MergeRecordList()
                    .Select(list => list.MergeSubRecords((subList, lon, lat) => new FileRecord3G
                    {
                        Longtitute = lon,
                        Lattitute = lat,
                        Pn = (short?) subList.FirstOrDefault(x => x.Pn != null)?.Pn,
                        RxAgc0 = subList.Where(x => x.RxAgc0 != null).Average(x => x.RxAgc0),
                        RxAgc1 = subList.Where(x => x.RxAgc1 != null).Average(x => x.RxAgc1),
                        RlpThroughput = (int?) subList.Where(x => x.RlpThroughput != null).Average(x => x.RlpThroughput),
                        TxAgc = subList.Where(x => x.TxAgc != null).Average(x => x.TxAgc),
                        StatTime = subList[0].StatTime,
                        TotalCi = subList.Where(x => x.TotalCi != null).Average(x => x.TotalCi),
                        DrcValue = (int?) subList.Where(x => x.DrcValue != null).Average(x => x.DrcValue),
                        Sinr = subList.Where(x => x.Sinr != null).Average(x => x.Sinr),
                        RasterNum = (short)((short)((lat-22.64409)/0.00895)*104+(short)((lon-112.387654)/0.0098))
                    }, (csv, stat) =>
                    {
                        if (csv.RxAgc0 != null) stat.RxAgc0 = csv.RxAgc0;
                        if (csv.RxAgc1 != null) stat.RxAgc1 = csv.RxAgc1;
                        if (csv.Sinr != null) stat.Sinr = csv.Sinr;
                        return stat.RxAgc0 != null && stat.RxAgc1 != null && stat.Sinr != null;
                    })).Aggregate((x, y) => x.Concat(y)).ToList();
        }

        public static List<FileRecordVolte> MergeRecords(this IEnumerable<FileRecordVolteCsv> csvs)
        {
            return
                csvs.MergeRecordList()
                    .Select(list => list.MergeSubRecords((subList, lon, lat) => new FileRecordVolte
                    {
                        Longtitute = lon,
                        Lattitute = lat,
                        Rsrp = subList.Where(x => x.Rsrp != null).Average(x => x.Rsrp),
                        Sinr = subList.Where(x => x.Sinr != null).Average(x => x.Sinr),
                        StatTime = subList[0].StatTime,
                        ENodebId = subList.FirstOrDefault(x => x.ENodebId != null)?.ENodebId,
                        SectorId = subList.FirstOrDefault(x => x.SectorId != null)?.SectorId,
                        PdcchPathloss = subList.Where(x => x.PdcchPathloss != null).Average(x => x.PdcchPathloss),
                        PdschPathloss = subList.Where(x => x.PdschPathloss != null).Average(x => x.PdschPathloss),
                        PucchTxPower = subList.Where(x => x.PucchTxPower != null).Average(x => x.PucchTxPower),
                        PucchPathloss = subList.Where(x => x.PucchPathloss != null).Average(x => x.PucchPathloss),
                        PuschPathloss = subList.Where(x => x.PuschPathloss != null).Average(x => x.PuschPathloss),
                        FirstEarfcn = subList.FirstOrDefault(x => x.FirstEarfcn != null)?.FirstEarfcn,
                        FirstPci = subList.FirstOrDefault(x => x.FirstPci != null)?.FirstPci,
                        FirstRsrp = subList.Where(x => x.FirstRsrp != null).Average(x => x.FirstRsrp),
                        SecondEarfcn = subList.FirstOrDefault(x => x.SecondEarfcn != null)?.SecondEarfcn,
                        SecondPci = subList.FirstOrDefault(x => x.SecondPci != null)?.SecondPci,
                        SecondRsrp = subList.Where(x => x.SecondRsrp != null).Average(x => x.SecondRsrp),
                        ThirdEarfcn = subList.FirstOrDefault(x => x.ThirdEarfcn != null)?.ThirdEarfcn,
                        ThirdPci = subList.FirstOrDefault(x => x.ThirdPci != null)?.ThirdPci,
                        ThirdRsrp = subList.Where(x => x.ThirdRsrp != null).Average(x => x.ThirdRsrp),
                        FourthEarfcn = subList.FirstOrDefault(x => x.FourthEarfcn != null)?.FourthEarfcn,
                        FourthPci = subList.FirstOrDefault(x => x.FourthPci != null)?.FourthPci,
                        FourthRsrp = subList.Where(x => x.FourthRsrp != null).Average(x => x.FourthRsrp),
                        FifthEarfcn = subList.FirstOrDefault(x => x.FifthEarfcn != null)?.FifthEarfcn,
                        FifthPci = subList.FirstOrDefault(x => x.FifthPci != null)?.FifthPci,
                        FifthRsrp = subList.Where(x => x.FifthRsrp != null).Average(x => x.FifthRsrp),
                        SixthEarfcn = subList.FirstOrDefault(x => x.SixthEarfcn != null)?.SixthEarfcn,
                        SixthPci = subList.FirstOrDefault(x => x.SixthPci != null)?.SixthPci,
                        SixthRsrp = subList.Where(x => x.SixthRsrp != null).Average(x => x.SixthRsrp),
                        RtpFrameType = subList.FirstOrDefault(x => x.RtpFrameType != null)?.RtpFrameType,
                        RtpLoggedPayloadSize = subList.Where(x => x.RtpLoggedPayloadSize != null).Average(x => x.RtpLoggedPayloadSize),
                        RtpPayloadSize = subList.Where(x => x.RtpPayloadSize != null).Average(x => x.RtpPayloadSize),
                        PolqaMos = subList.Where(x => x.PolqaMos != null).Average(x => x.PolqaMos),
                        PacketLossCount = subList.Where(x => x.PacketLossCount != null).Average(x => x.PacketLossCount),
                        RxRtpPacketNum = subList.Where(x => x.RxRtpPacketNum != null).Average(x => x.RxRtpPacketNum),
                        VoicePacketDelay = subList.Where(x => x.VoicePacketDelay != null).Average(x => x.VoicePacketDelay),
                        VoicePacketLossRate = subList.Where(x => x.VoicePacketLossRate != null).Average(x => x.VoicePacketLossRate),
                        VoiceJitter = subList.Where(x => x.VoiceJitter != null).Average(x => x.VoiceJitter),
                        Rank2Cqi = subList.Where(x => x.Rank2Cqi != null).Average(x => x.Rank2Cqi),
                        Rank1Cqi = subList.Where(x => x.Rank1Cqi != null).Average(x => x.Rank1Cqi),
                        RasterNum = (short)((short)((lat - 22.64409) / 0.00895) * 104 + (short)((lon - 112.387654) / 0.0098))
                    }, (csv, stat) =>
                    {
                        if (csv.Rsrp != null) stat.Rsrp = csv.Rsrp;
                        if (csv.Sinr != null) stat.Sinr = csv.Sinr;
                        return stat.Rsrp != null && stat.Sinr != null;
                    })).Aggregate((x, y) => x.Concat(y)).ToList();
        }

        public static List<FileRecord4G> MergeRecords(this IEnumerable<FileRecord4GCsv> csvs, DateTime? statDate = null)
        {
            return
                csvs.MergeRecordList()
                    .Select(list => list.MergeSubRecords((subList, lon, lat) => new FileRecord4G
                    {
                        Longtitute = lon,
                        Lattitute = lat,
                        Pci = (short?)subList.FirstOrDefault(x => x.Pn != null)?.Pn,
                        ENodebId = subList.FirstOrDefault(x => x.ENodebId != null)?.ENodebId,
                        SectorId = subList.FirstOrDefault(x => x.SectorId != null)?.SectorId,
                        Frequency = subList.FirstOrDefault(x => x.Frequency != null)?.Frequency,
                        Rsrp = subList.Where(x => x.Rsrp != null).Average(x => x.Rsrp),
                        DlBler = (byte?)subList.Where(x => x.DlBler != null).Average(x => x.DlBler),
                        N1Pci = subList.FirstOrDefault(x => x.N1Pci != null)?.N1Pci,
                        N1Rsrp = subList.FirstOrDefault(x => x.N1Rsrp != null)?.N1Rsrp,
                        N2Pci = subList.FirstOrDefault(x => x.N2Pci != null)?.N2Pci,
                        N2Rsrp = subList.FirstOrDefault(x => x.N2Rsrp != null)?.N2Rsrp,
                        N3Pci = subList.FirstOrDefault(x => x.N3Pci != null)?.N3Pci,
                        N3Rsrp = subList.FirstOrDefault(x => x.N3Rsrp != null)?.N3Rsrp,
                        PdcpThroughputDl = subList.Where(x => x.PdcpThroughputDl != null).Average(x => x.PdcpThroughputDl),
                        PdcpThroughputUl = subList.Where(x => x.PdcpThroughputUl != null).Average(x => x.PdcpThroughputUl),
                        MacThroughputDl = subList.Where(x => x.MacThroughputDl != null).Average(x => x.MacThroughputDl),
                        PhyThroughputDl = subList.Where(x => x.PhyThroughputDl != null).Average(x => x.PhyThroughputDl),
                        DlMcs = (byte?)subList.Where(x => x.DlMcs != null).Average(x => x.DlMcs),
                        UlMcs = (byte?)subList.Where(x => x.UlMcs != null).Average(x => x.UlMcs),
                        StatTime = statDate ?? subList[0].StatTime,
                        CqiAverage = subList.Where(x => x.CqiAverage != null).Average(x => x.CqiAverage),
                        PdschRbSizeAverage = (int?)subList.Where(x => x.PdschRbSizeAverage != null).Average(x => x.PdschRbSizeAverage),
                        PuschRbSizeAverage = (int?)subList.Where(x => x.PuschRbSizeAverage != null).Average(x => x.PuschRbSizeAverage),
                        PuschRbNum = (int?)subList.Where(x => x.PuschRbNum != null).Average(x => x.PuschRbNum),
                        PdschRbNum = (int?)subList.Where(x => x.PdschRbNum != null).Average(x => x.PdschRbNum),
                        Sinr = subList.Where(x => x.Sinr != null).Average(x => x.Sinr),
                        RasterNum = (short)((short)((lat - 22.64409) / 0.00895) * 104 + (short)((lon - 112.387654) / 0.0098))
                    }, (csv, stat) =>
                    {
                        if (csv.Rsrp != null) stat.Rsrp = csv.Rsrp;
                        if (csv.Sinr != null) stat.Sinr = csv.Sinr;
                        return stat.Rsrp != null && stat.Sinr != null;
                    })).Aggregate((x, y) => x.Concat(y)).ToList();
        }

        public static List<FileRecord4G> MergeRecords(this IEnumerable<FileRecord4GDingli> csvs, DateTime? statDate = null)
        {
            return
                csvs.MergeRecordList()
                    .Select(list => list.MergeSubRecords((subList, lon, lat) => new FileRecord4G
                    {
                        Longtitute = lon,
                        Lattitute = lat,
                        Pci = (short?)subList.FirstOrDefault(x => x.Pn != null)?.Pn,
                        ENodebId = subList.FirstOrDefault(x => x.ENodebId != null)?.ENodebId,
                        SectorId = subList.FirstOrDefault(x => x.SectorId != null)?.SectorId,
                        Frequency = subList.FirstOrDefault(x => x.Frequency != null)?.Frequency,
                        Rsrp = subList.Where(x => x.Rsrp != null).Average(x => x.Rsrp),
                        DlBler = (byte?)subList.Where(x => x.DlBler != null).Average(x => x.DlBler),
                        N1Pci = subList.FirstOrDefault(x => x.N1Pci != null)?.N1Pci,
                        N1Rsrp = subList.FirstOrDefault(x => x.N1Rsrp != null)?.N1Rsrp,
                        N2Pci = subList.FirstOrDefault(x => x.N2Pci != null)?.N2Pci,
                        N2Rsrp = subList.FirstOrDefault(x => x.N2Rsrp != null)?.N2Rsrp,
                        N3Pci = subList.FirstOrDefault(x => x.N3Pci != null)?.N3Pci,
                        N3Rsrp = subList.FirstOrDefault(x => x.N3Rsrp != null)?.N3Rsrp,
                        PdcpThroughputDl = subList.Where(x => x.PdcpThroughputDl != null).Average(x => x.PdcpThroughputDl),
                        PdcpThroughputUl = subList.Where(x => x.PdcpThroughputUl != null).Average(x => x.PdcpThroughputUl),
                        MacThroughputDl = subList.Where(x => x.MacThroughputDl != null).Average(x => x.MacThroughputDl),
                        PhyThroughputDl = subList.Where(x => x.PhyThroughputDl != null).Average(x => x.PhyThroughputDl),
                        DlMcs = (byte?)subList.Where(x => x.DlMcs != null).Average(x => x.DlMcs),
                        UlMcs = (byte?)subList.Where(x => x.UlMcs != null).Average(x => x.UlMcs),
                        StatTime = statDate ?? subList[0].StatTime,
                        CqiAverage = subList.Where(x => x.CqiAverage != null).Average(x => x.CqiAverage),
                        PdschRbSizeAverage = (int?)subList.Where(x => x.PdschRbSizeAverage != null).Average(x => x.PdschRbSizeAverage),
                        PuschRbSizeAverage = (int?)subList.Where(x => x.PuschRbSizeAverage != null).Average(x => x.PuschRbSizeAverage),
                        PuschRbNum = (int?)subList.Where(x => x.PuschRbNum != null).Average(x => x.PuschRbNum),
                        PdschRbNum = (int?)subList.Where(x => x.PdschRbNum != null).Average(x => x.PdschRbNum),
                        Sinr = subList.Where(x => x.Sinr != null).Average(x => x.Sinr),
                        RasterNum = (short)((short)((lat - 22.64409) / 0.00895) * 104 + (short)((lon - 112.387654) / 0.0098))
                    }, (csv, stat) =>
                    {
                        if (csv.Rsrp != null) stat.Rsrp = csv.Rsrp;
                        if (csv.Sinr != null) stat.Sinr = csv.Sinr;
                        return stat.Rsrp != null && stat.Sinr != null;
                    })).Aggregate((x, y) => x.Concat(y)).ToList();
        }

        public static List<List<TCsv>> MergeRecordList<TCsv>(this IEnumerable<TCsv> csvs)
            where TCsv : IPn
        {
            var results = new List<List<TCsv>>();
            var tempCsvs = new List<TCsv>();
            var lastPn = -1;
            foreach (var csv in csvs)
            {
                if (csv.Pn == null || lastPn == (int)csv.Pn)
                {
                    tempCsvs.Add(csv);
                }
                else
                {
                    if (tempCsvs.Any()) results.Add(tempCsvs);
                    tempCsvs = new List<TCsv> {csv};
                    lastPn = (int) csv.Pn;
                }
            }
            if (tempCsvs.Any()) results.Add(tempCsvs);
            return results;
        }

        public static IEnumerable<TStat> MergeSubRecords<TCsv, TStat>(this List<TCsv> tempCsvs,
            Func<List<TCsv>, double, double, TStat> generateFunc, Func<TCsv, TStat, bool> validateFunc)
            where TCsv : IGeoPoint<double?>, IStatTime
            where TStat : class, new()
        {
            if (!tempCsvs.Any()) return new List<TStat>();
            var results = new List<TStat>();
            var lon = tempCsvs[0].Longtitute ?? 112.0;
            var lat = tempCsvs[0].Lattitute ?? 22.0;
            var subList = new List<TCsv>();
            var stat = new TStat();
            foreach (var csv in tempCsvs)
            {
                var lo = csv.Longtitute ?? 112.0;
                var la = csv.Lattitute ?? 22.0;
                var validate = validateFunc(csv, stat);
                if ((lo > lon - 0.000049 && lo < lon + 0.000049 && la > lat - 0.000045 && la < lat + 0.000045)
                    ||(lo > lon - 0.00098 && lo < lon + 0.00098 && la > lat - 0.0009 && la < lat + 0.0009 && !validate))
                {
                    subList.Add(csv);
                }
                else
                {
                    results.Add(generateFunc(subList, lon, lat));
                    subList = new List<TCsv> {csv};
                    lon = lo;
                    lat = la;
                }
            }
            if (subList.Any())
            {
                results.Add(generateFunc(subList, lon, lat));
            }
            return results;
        }

        public static string GenerateInsertSql(this FileRecord2G stat, string tableName)
        {
            return "INSERT INTO [" + tableName
                   + "] ( [rasterNum],[testTime],[lon],[lat],[refPN],[EcIo],[rxAGC],[txAGC],[txPower],[txGain]) VALUES("
                   + stat.RasterNum
                   + ",'" + stat.StatTime
                   + "'," + stat.Longtitute
                   + "," + stat.Lattitute
                   + "," + (stat.Pn == null ? "NULL" : stat.Pn.ToString())
                   + "," + (stat.Ecio == null ? "NULL" : stat.Ecio.ToString())
                   + "," + (stat.RxAgc == null ? "NULL" : stat.RxAgc.ToString())
                   + "," + (stat.TxAgc == null ? "NULL" : stat.TxAgc.ToString())
                   + "," + (stat.TxPower == null ? "NULL" : stat.TxPower.ToString())
                   + "," + (stat.TxGain == null ? "NULL" : stat.TxGain.ToString()) + ")";
        }

        public static string GenerateInsertSql(this FileRecord3G stat, string tableName)
        {
            return "INSERT INTO [" + tableName
                   + "] ( [rasterNum],[testTime],[lon],[lat],[refPN],[SINR],[rxAGC0],[rxAGC1],[txAGC],[totalC2I],[DRCValue],[RLPThrDL]) VALUES("
                   + stat.RasterNum
                   + ",'" + stat.StatTime
                   + "'," + stat.Longtitute
                   + "," + stat.Lattitute
                   + "," + (stat.Pn == null ? "NULL" : stat.Pn.ToString())
                   + "," + (stat.Sinr == null ? "NULL" : stat.Sinr.ToString())
                   + "," + (stat.RxAgc0 == null ? "NULL" : stat.RxAgc0.ToString())
                   + "," + (stat.RxAgc0 == null ? "NULL" : stat.RxAgc0.ToString())
                   + "," + (stat.TxAgc == null ? "NULL" : stat.TxAgc.ToString())
                   + "," + (stat.TotalCi == null ? "NULL" : stat.TotalCi.ToString())
                   + "," + (stat.DrcValue == null ? "NULL" : stat.DrcValue.ToString())
                   + "," + (stat.RlpThroughput == null ? "NULL" : stat.RlpThroughput.ToString()) + ")";
        }

        public static string GenerateInsertSql(this FileRecord4G stat, string tableName, int index)
        {
            return "INSERT INTO [" + tableName
                   + "] ( [ind],[rasterNum],[testTime],[lon],[lat],[eNodeBID],[cellID],[freq],[PCI],"
                   + "[RSRP],[SINR],[DLBler],[CQIave],[ULMCS],[DLMCS],[PDCPThrUL],[PDCPThrDL],[PHYThrDL],[MACThrDL],"
                   + "[PUSCHRbNum],[PDSCHRbNum],[PUSCHTBSizeAve],[PDSCHTBSizeAve],[n1PCI],[n1RSRP],[n2PCI],[n2RSRP],[n3PCI],[n3RSRP]) VALUES("
                   + index  //[ind]
                   + "," + stat.RasterNum  //[rasterNum]
                   + ",'" + stat.StatTime //[testTime]
                   + "'," + stat.Longtitute
                   + "," + stat.Lattitute
                   + "," + (stat.ENodebId == null ? "NULL" : stat.ENodebId.ToString())
                   + "," + (stat.SectorId == null ? "NULL" : stat.SectorId.ToString())
                   + "," + (stat.Frequency == null ? "NULL" : stat.Frequency.ToString())
                   + "," + (stat.Pci == null ? "NULL" : stat.Pci.ToString())
                   + "," + (stat.Rsrp == null ? "NULL" : stat.Rsrp.ToString())
                   + "," + (stat.Sinr == null ? "NULL" : stat.Sinr.ToString())
                   + "," + (stat.DlBler == null ? "NULL" : stat.DlBler.ToString())
                   + "," + (stat.CqiAverage == null ? "NULL" : stat.CqiAverage.ToString())
                   + "," + (stat.UlMcs == null ? "NULL" : stat.UlMcs.ToString())
                   + "," + (stat.DlMcs == null ? "NULL" : stat.DlMcs.ToString())
                   + "," + (stat.PdcpThroughputUl == null ? "NULL" : stat.PdcpThroughputUl.ToString())
                   + "," + (stat.PdcpThroughputDl == null ? "NULL" : stat.PdcpThroughputDl.ToString())
                   + "," + (stat.PhyThroughputDl == null ? "NULL" : stat.PhyThroughputDl.ToString())
                   + "," + (stat.MacThroughputDl == null ? "NULL" : stat.MacThroughputDl.ToString())
                   + "," + (stat.PuschRbNum == null ? "NULL" : stat.PuschRbNum.ToString())
                   + "," + (stat.PdschRbNum == null ? "NULL" : stat.PdschRbNum.ToString())
                   + "," + (stat.PuschRbSizeAverage == null ? "NULL" : stat.PuschRbSizeAverage.ToString())
                   + "," + (stat.PdschRbSizeAverage == null ? "NULL" : stat.PdschRbSizeAverage.ToString())
                   + "," + (stat.N1Pci == null ? "NULL" : stat.N1Pci.ToString())
                   + "," + (stat.N1Rsrp == null ? "NULL" : stat.N1Rsrp.ToString())
                   + "," + (stat.N2Pci == null ? "NULL" : stat.N2Pci.ToString())
                   + "," + (stat.N2Rsrp == null ? "NULL" : stat.N2Rsrp.ToString())
                   + "," + (stat.N3Pci == null ? "NULL" : stat.N3Pci.ToString())
                   + "," + (stat.N3Rsrp == null ? "NULL" : stat.N3Rsrp.ToString()) + ")";
        }

        public static string GenerateInsertSql(this FileRecordVolte stat, string tableName)
        {
            return "INSERT INTO [" + tableName
                   + "] ( [rasterNum],[testTime],[lon],[lat],[eNodeBID],[cellID],[RSRP],[SINR],[PDCCHPathloss],[PDSCHPathloss],"
                   + "[PUCCHTxPower],[PUCCHPathloss],[PUSCHPathloss],[Cell1stEARFCN],[Cell1stPCI],[Cell1stRSRP],"
                   + "[Cell2ndEARFCN],[Cell2ndPCI],[Cell2ndRSRP],[Cell3rdEARFCN],[Cell3rdPCI],[Cell3rdRSRP],"
                   + "[Cell4thEARFCN],[Cell4thPCI],[Cell4thRSRP],[Cell5thEARFCN],[Cell5thPCI],[Cell5thRSRP],"
                   + "[Cell6thEARFCN],[Cell6thPCI],[Cell6thRSRP],[RTPFrameType],[RTPLoggedPayloadSize],"
                   + "[RTPPayloadSize],[PolqaMos],[PacketLossCount],[RxRTPPacketNum],[VoicePacketDelay],"
                   + "[VoicePacketLossRate],[VoiceRFC1889Jitter],[Rank2CQICode0],[Rank1CQI]) VALUES("
                   + stat.RasterNum
                   + ",'" + stat.StatTime
                   + "'," + stat.Longtitute
                   + "," + stat.Lattitute
                   + "," + (stat.ENodebId == null ? "NULL" : stat.ENodebId.ToString())
                   + "," + (stat.SectorId == null ? "NULL" : stat.SectorId.ToString())
                   + "," + (stat.Rsrp == null ? "NULL" : stat.Rsrp.ToString())
                   + "," + (stat.Sinr == null ? "NULL" : stat.Sinr.ToString())
                   + "," + (stat.PdcchPathloss == null ? "NULL" : stat.PdcchPathloss.ToString())
                   + "," + (stat.PdschPathloss == null ? "NULL" : stat.PdschPathloss.ToString())
                   + "," + (stat.PucchTxPower == null ? "NULL" : stat.PucchTxPower.ToString())
                   + "," + (stat.PucchPathloss == null ? "NULL" : stat.PucchPathloss.ToString())
                   + "," + (stat.PuschPathloss == null ? "NULL" : stat.PuschPathloss.ToString())
                   + "," + (stat.FirstEarfcn == null ? "NULL" : stat.FirstEarfcn.ToString())
                   + "," + (stat.FirstPci == null ? "NULL" : stat.FirstPci.ToString())
                   + "," + (stat.FirstRsrp == null ? "NULL" : stat.FirstRsrp.ToString())
                   + "," + (stat.SecondEarfcn == null ? "NULL" : stat.SecondEarfcn.ToString())
                   + "," + (stat.SecondPci == null ? "NULL" : stat.SecondPci.ToString())
                   + "," + (stat.SecondRsrp == null ? "NULL" : stat.SecondRsrp.ToString())
                   + "," + (stat.ThirdEarfcn == null ? "NULL" : stat.ThirdEarfcn.ToString())
                   + "," + (stat.ThirdPci == null ? "NULL" : stat.ThirdPci.ToString())
                   + "," + (stat.ThirdRsrp == null ? "NULL" : stat.ThirdRsrp.ToString())
                   + "," + (stat.FourthEarfcn == null ? "NULL" : stat.FourthEarfcn.ToString())
                   + "," + (stat.FourthPci == null ? "NULL" : stat.FourthPci.ToString())
                   + "," + (stat.FourthRsrp == null ? "NULL" : stat.FourthRsrp.ToString())
                   + "," + (stat.FifthEarfcn == null ? "NULL" : stat.FifthEarfcn.ToString())
                   + "," + (stat.FifthPci == null ? "NULL" : stat.FirstPci.ToString())
                   + "," + (stat.FifthRsrp == null ? "NULL" : stat.FifthRsrp.ToString())
                   + "," + (stat.SixthEarfcn == null ? "NULL" : stat.SixthEarfcn.ToString())
                   + "," + (stat.SixthPci == null ? "NULL" : stat.SixthPci.ToString())
                   + "," + (stat.SixthRsrp == null ? "NULL" : stat.SixthRsrp.ToString())
                   + "," + (stat.RtpFrameType == null ? "NULL" : stat.RtpFrameType.ToString())
                   + "," + (stat.RtpLoggedPayloadSize == null ? "NULL" : stat.RtpLoggedPayloadSize.ToString())
                   + "," + (stat.RtpPayloadSize == null ? "NULL" : stat.RtpPayloadSize.ToString())
                   + "," + (stat.PolqaMos == null ? "NULL" : stat.PolqaMos.ToString())
                   + "," + (stat.PacketLossCount == null ? "NULL" : stat.PacketLossCount.ToString())
                   + "," + (stat.RxRtpPacketNum == null ? "NULL" : stat.RxRtpPacketNum.ToString())
                   + "," + (stat.VoicePacketDelay == null ? "NULL" : stat.VoicePacketDelay.ToString())
                   + "," + (stat.VoicePacketLossRate == null ? "NULL" : stat.VoicePacketLossRate.ToString())
                   + "," + (stat.VoiceJitter == null ? "NULL" : stat.VoiceJitter.ToString())
                   + "," + (stat.Rank2Cqi == null ? "NULL" : stat.Rank2Cqi.ToString())
                   + "," + (stat.Rank1Cqi == null ? "NULL" : stat.Rank1Cqi.ToString()) + ")";
        }

    }
}
