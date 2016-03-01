using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalTest : IDisposable
  {
    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Animal.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_overrideTrueForSameAnimal()
    {
      DateTime testDate = new DateTime(2016, 3, 2);
      Animal firstAnimal = new Animal("Buddy", "male", testDate, "Dog", 1);
      Animal secondAnimal = new Animal("Buddy", "male", testDate, "Dog", 1);

      Assert.Equal(firstAnimal, secondAnimal);
    }

    [Fact]
    public void Test_Save()
    {
      DateTime testDate = new DateTime(2016, 3, 2);
      Animal testAnimal = new Animal("Buddy", "male", testDate, "Dog", 1);
      testAnimal.Save();

      List<Animal> result = Animal.GetAll();
      List<Animal> testList = new List<Animal>{testAnimal};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignIdToObject()
    {
      DateTime testDate = new DateTime(2016, 3, 2);
      Animal testAnimal = new Animal("Buddy", "male", testDate, "Dog", 1);

      testAnimal.Save();

      List<Animal> testSaveAnimals = Animal.GetAll();

      Animal savedAnimal = Animal.GetAll()[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsAnimalInDatabase()
    {
      DateTime testDate = new DateTime(2016, 3, 2);
      Animal testAnimal = new Animal("Buddy", "male", testDate, "Dog", 1);
      testAnimal.Save();

      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      Assert.Equal(testAnimal, foundAnimal);
    }

    public void Dispose()
    {
      Animal.DeleteAll();
    }
  }
}
