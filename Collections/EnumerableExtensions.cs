using System;
using System.Collections.Generic;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IList<T>> ToBatchEnumerable<T>(this IEnumerable<T> list, int batchSize)
        {
            if(batchSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(batchSize));
            }

            IList<T> batch = new List<T>(batchSize);

            foreach (T item in list)
            {
                if(batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<T>();
                }

                batch.Add(item);
            }

            if(batch.Count > 0)
            {
                yield return batch;
            }
        }

        public static IList<T>[] ToBins<T>(this IList<T> list, int numberOfBins)
        {
            if(numberOfBins <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfBins));
            }

            int batchSize = list.Count / numberOfBins;

            if(list.Count % numberOfBins > 0)
            {
                batchSize++;
            }

            if(batchSize * numberOfBins >= list.Count + batchSize)
            {
                throw new InvalidOperationException($"Not possible to separate {list.Count} list into {numberOfBins} bins.");
            }

            IEnumerable<IList<T>> enumerable = list.ToBatchEnumerable(batchSize);
            IList<T>[] bins = new IList<T>[numberOfBins];
            int index = 0;

            foreach (IList<T> batch in enumerable)
            {
                bins[index] = batch;
                index++;
            }

            return bins;
        }
    }
}
