using System;
using System.Windows.Controls;
using System.Windows.Data;

// make compatible with PresentationFramework.dll internal interfaces

namespace System.Windows
{
    //non-internal 
    public interface IGeneratorHost
    {
        /// <summary>
        /// The view of the data
        /// </summary>
        ItemCollection View { get; }

        /// <summary>
        /// Return true if the item is (or should be) its own item container
        /// </summary>
        bool IsItemItsOwnContainer(object item);

        /// <summary>
        /// Return the element used to display the given item
        /// </summary>
        DependencyObject GetContainerForItem(object item);

        /// <summary>
        /// Prepare the element to act as the ItemUI for the corresponding item.
        /// </summary>
        void PrepareItemContainer(DependencyObject container, object item);

        /// <summary>
        /// Undo any initialization done on the element during GetContainerForItem and PrepareItemContainer
        /// </summary>
        void ClearContainerForItem(DependencyObject container, object item);

        /// <summary>
        /// Determine if the given element was generated for this host as an ItemUI.
        /// </summary>
        bool IsHostForItemContainer(DependencyObject container);

        /// <summary>
        /// Return the GroupStyle (if any) to use for the given group at the given level.
        /// </summary>
        GroupStyle GetGroupStyle(CollectionViewGroup group, int level);

        /// <summary>
        /// Communicates to the host that the generator is using grouping.
        /// </summary>
        void SetIsGrouping(bool isGrouping);

        /// <summary>
        /// The AlternationCount
        /// <summary>
        int AlternationCount { get; }
    }

    public interface IContainItemStorage
    {
        ///// <summary>
        ///// Stores the given value in ItemValueStorage, associating it with the given item and DependencyProperty.
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="dp">DependencyProperty</param>
        ///// <param name="value"></param>
        //void StoreItemValue(object item, DependencyProperty dp, object value);

        ///// <summary>
        ///// Returns the value storaed gainst the given DependencyProperty and item.
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="dp"></param>
        ///// <returns></returns>
        //object ReadItemValue(object item, DependencyProperty dp);

        ///// <summary>
        ///// Clears the value in ItemValueStorage, associating it with the given item and DependencyProperty.
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="dp">DependencyProperty</param>
        //void ClearItemValue(object item, DependencyProperty dp);

        ///// <summary>
        ///// Clears the given DependencyProperty starting at the current element including all nested storage bags.
        ///// </summary>
        ///// <param name="dp">DependencyProperty</param>
        //void ClearValue(DependencyProperty dp);

        /// <summary>
        /// Clears the item storage on the current element entirely.
        /// </summary>
        void Clear();
    }

}