using System.Collections.ObjectModel;

namespace Example_IDataErrorInfo
{
    public static class TestData
    {
        public static DataParents CreateDataItems(int parent, int children)
        {
            DataParents dataItems = new();

            for (int i = 0; i < parent; i++)
            {
                DataParent dataItem = new()
                {
                    ParentName = "Parent" + i,
                    ParentAge = i,
                    Children = CreateChildren(children)
                };

                dataItems.Add(dataItem);
            }

            return dataItems;
        }

        public static ObservableCollection<DataChildren> CreateChildren(int children)
        {
            ObservableCollection<DataChildren> Children = new();

            for (int i = 0; i < children; i++)
            {
                DataChildren dataChildren = new()
                {
                    ChildName = "ChildName" + i,
                    ChildAge = i,
                };

                Children.Add(dataChildren);
            }

            return Children;
        }
    }
}
