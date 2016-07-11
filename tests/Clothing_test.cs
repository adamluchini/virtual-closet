using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Closet.Objects
{
  public class TshirtTest : IDisposable
  {
    public TshirtTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=closet_database;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Tshirt.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Tshirt.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfColorsAreTheSame()
    {
      //Arrange, Act
      Tshirt firstTshirt = new Tshirt("red");
      Tshirt secondTshirt = new Tshirt("red");

      //Assert
      Assert.Equal(firstTshirt, secondTshirt);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Tshirt testTshirt = new Tshirt("green");

      //Act
      testTshirt.Save();
      List<Tshirt> result = Tshirt.GetAll();
      List<Tshirt> testList = new List<Tshirt>{testTshirt};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignIdToObject()
    {

      //Arrange
      Tshirt testTshirt = new Tshirt("blue");

      //Act
      testTshirt.Save();
      Tshirt savedTshirt = Tshirt.GetAll()[0];

      int result = savedTshirt.GetId();
      int testId = testTshirt.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsTshirtInDatabase()
    {
      //Arrange
      Tshirt testTshirt = new Tshirt("green");
      testTshirt.Save();

      //Act
      Tshirt foundTshirt = Tshirt.Find(testTshirt.GetId());

      //Assert
      Assert.Equal(testTshirt, foundTshirt);
    }
  }
}
