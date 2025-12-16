namespace IMSClassLib
{
    public class InventoryManager
    {
        // This method checks the quantity and returns a status code
        // 1 = High, 0 = Medium, -1 = Low
        public int CheckStockStatus(int quantity)
        {
            if (quantity > 50)
            {
                return 1; // High Stock
            }
            else if (quantity > 10)
            {
                return 0; // Medium Stock
            }
            else
            {
                return -1; // Low Stock
            }
        }
    }
}