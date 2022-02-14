using System.Collections.ObjectModel;

namespace Example_IDataErrorInfo
{
    public static class TestData
    {
        public static DataProducts CreateDataItems(int products)
        {
            DataProducts dataItems = new();

            for (int i = 0; i < products; i++)
            {
                DataProduct dataItem = new()
                {
                    Product = "Product" + i,
                    Stock = i,
                };

                if (i == 1)
                {
                    dataItem.Product = "";
                }

                dataItems.Add(dataItem);
            }

            return dataItems;
        }
    }
}
