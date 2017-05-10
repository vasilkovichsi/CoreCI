using CoreCI.Models.BuildItem;

namespace CoreCI.Logic.BuildQueue
{
    internal interface IBuildQueue
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        bool Add(BuildItem item);

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <returns></returns>
        BuildItem GetNext();

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        bool Remove(BuildItem item);
    }
}