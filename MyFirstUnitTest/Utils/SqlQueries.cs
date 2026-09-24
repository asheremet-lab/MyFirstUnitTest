namespace MyFirstUnitTest.Utils;

public static class SqlQueries
{
    public const string GetAllCategories = "SELECT * FROM Categories";

    public const string GetProductById = "SELECT * FROM Products WHERE Id = @Id";

    public const string GetOrderByIdAndUserId = "SELECT * FROM Orders WHERE Id = @OrderId AND UserId = @UserId";

    public const string GetProductsByOrderId = @"
        SELECT p.* 
        FROM Products p
        INNER JOIN OrderItems oi ON p.Id = oi.ProductId
        WHERE oi.OrderId = @OrderId";

    // HW 18-19: Города покупателей аксессуаров
    public const string GetCitiesBuyingAccessories = @"
        SELECT DISTINCT u.City 
        FROM Users u
        JOIN Orders o ON u.Id = o.UserId
        JOIN OrderItems oi ON o.Id = oi.OrderId
        JOIN Products p ON oi.ProductId = p.Id
        JOIN Categories c ON p.CategoryId = c.Id
        WHERE c.Name = 'Аксессуары'";

    // HW 18-19: Покупатели телевизоров и аксессуаров
    public const string GetTvBuyersWhoAlsoBoughtAccessories = @"
        SELECT DISTINCT u.Id 
        FROM Users u
        JOIN Orders o ON u.Id = o.UserId
        JOIN OrderItems oi ON o.Id = oi.OrderId
        JOIN Products p ON oi.ProductId = p.Id
        JOIN Categories c ON p.CategoryId = c.Id
        WHERE c.Name = 'Телевизоры' 
          AND u.Id IN (
              SELECT u2.Id 
              FROM Users u2
              JOIN Orders o2 ON u2.Id = o2.UserId
              JOIN OrderItems oi2 ON o2.Id = oi2.OrderId
              JOIN Products p2 ON oi2.ProductId = p2.Id
              JOIN Categories c2 ON p2.CategoryId = c2.Id
              WHERE c2.Name = 'Аксессуары'
          )";
}