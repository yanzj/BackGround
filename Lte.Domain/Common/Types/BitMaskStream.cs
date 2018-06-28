using System.Collections.Generic;

namespace Lte.Domain.Common.Types
{
    public class BitMaskStream
    {
        private readonly Queue<bool> _list;

        public BitMaskStream(BitArrayInputStream input, int count)
        {
            _list = new Queue<bool>(count);
            for (int i = 0; i < count; i++)
            {
                _list.Enqueue(input.ReadBit() != 0);
            }
        }

        public bool Read()
        {
            return _list.Dequeue();
        }
    }
}
