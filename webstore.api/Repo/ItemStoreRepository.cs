using Microsoft.Data.SqlClient;
using webstore.api.Models;

namespace webstore.api.Data;

public class ItemStoreRepository
{
    private readonly string _connectionString;
    private List<Item> _items = new List<Item>();

    private List<Category> _categories = new List<Category>();

    public ItemStoreRepository()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();

        _connectionString = config.GetConnectionString("MyDBConnection");

        InitializeCategories();
        InitializeItems();
    }  

    public void InitializeCategories()
    {
        List<Category> result = new List<Category>();
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"EXEC sp_ReadCategoriesFromDB",con);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int categoryID = dr["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CategoryID"]);
                    string name = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]);
                    result.Add(new Category { CategoryID = categoryID, Name = name });
                }
            } 
            con.Close();
        } 
        _categories = result;
    } 

    public void InitializeItems()
    {
        List<Item> result = new List<Item>();
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"EXEC sp_ReadItemsFromDB",con);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int itemID = dr["ItemID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ItemID"]);
                    string name = dr["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemName"]);
                    decimal price = dr["Price"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Price"]);
                    int categoryID = dr["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CategoryID"]);
                    result.Add(new Item { ItemID = itemID, Name = name, Price = price, CategoryID = categoryID });
                }
            }
            con.Close();
        }
        _items = result;
    }
}