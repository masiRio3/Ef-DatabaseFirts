

using Ef_DatabaseFirts.Models;
using EFDatabaseFirst.Namespace;
using Microsoft.EntityFrameworkCore;

var ctx = new NorthwindContext();

////Creamos el customer
//var newCustomer = new Customer()
//{
//    CustomerId = "Mauri".ToUpper(),
//    CompanyName = "Mauricio Demasi",
//    Orders = new List<Order>()

//};


//newCustomer.Orders.Add(new Order()
//{
//    CustomerId = newCustomer.CustomerId.ToUpper(),
//    OrderDate = DateTime.Now
//});

//////Agregamos el customer
//ctx.Add(newCustomer);
//ctx.SaveChanges();

//EDITAR UN CUSTOMER

//var reg = ctx.Customers.FirstOrDefault(r=> r.CustomerId=="mauri".ToUpper());
//if (reg != null)
//{
//    reg.CompanyName = "Mauri Demasi Editado";
//    ctx.SaveChanges();
//}

//ELIMINAR UN CUSTOMER
// AL TENER UNA CONSTRAINT CON LA TABLA ORDENES PRIMERO DEBEMOS ELIMINAR ESTAS
var regOrders = ctx.Orders.Where(r => r.CustomerId == "Mauri".ToUpper());
ctx.RemoveRange(regOrders);
// AHORA SI ELIMINAMOS EL CUSTOMER
var regCustomer = ctx.Customers.FirstOrDefault(r => r.CustomerId == "mauri".ToUpper());
ctx.Remove(regCustomer);
ctx.SaveChanges();



//var customers = ctx.Customers;

//var sql = ctx.Customers.FromSqlRaw("SELECT * FROM Customers");



var customers = ctx.Customers.Select(selector =>
                                            new
                                            {
                                                IdCustomer = selector.CustomerId,
                                                NameCustomer = selector.CompanyName
                                            });
//De esta manera traemos solo lo que necesitamos SELECT

Console.WriteLine("Lista de Customers");
Console.WriteLine("========================");
foreach (var item in customers)
{
    //Console.WriteLine($"{item.CustomerId} - {item.CompanyName}");

    Console.WriteLine($"{item.IdCustomer} - {item.NameCustomer}");
}

Console.WriteLine("=====================");
Console.WriteLine("");

Console.WriteLine("INGRESE EL IdCustomer que desea consultar");
string idCustomer = Console.ReadLine();

//Consulta si existe en la bd (Any)
bool anyCustomer = ctx.Customers.Any(selector => selector.CustomerId == idCustomer.ToUpper());

if (anyCustomer)
{
    Console.WriteLine("El Customer existe");


    //    exec sp_executesql N'SELECT [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
    //FROM(
    //    SELECT TOP(1)[c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    //    FROM[Customers] AS[c]
    //    WHERE[c].[CustomerID] = @__idCustomer_0
    //) AS[t]
    //LEFT JOIN[Orders] AS[o] ON[t].[CustomerID] = [o].[CustomerID]
    //ORDER BY[t].[CustomerID] ',N'@__idCustomer_0 nchar(5)',@__idCustomer_0=N'wolza'
    //SELECT TOP 
    var customer = ctx.Customers.Include(i=> i.Orders)
                            .FirstOrDefault(selector => selector.CustomerId == idCustomer);
    Console.WriteLine();
    Console.WriteLine($"IdCustomer: { customer.CustomerId} - CompanyName: {customer.CompanyName} ");
    Console.WriteLine();
    Console.WriteLine("Orders");

    foreach (var item in customer.Orders)
    {
        Console.WriteLine($"Order Id: {item.OrderId} - OrderDate:{item.OrderDate} ");
    }
    Console.WriteLine();
}
else
{
    Console.WriteLine("El Customer no existe");
}


Console.ReadKey();
