// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//CustomeList

var list = new CustomList<int>();

list.Add(2);
list.Add(5);
list.Add(10);

foreach (var item in list)
{
    Console.WriteLine(item);

}

list.RemoveAt(1);

//list.RemoveAt(10);

foreach(var item in list)
{
    System.Console.WriteLine(item);
}