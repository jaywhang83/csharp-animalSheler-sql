using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalTypeTest : IDisposable
  {
    public AnimalTypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security =SSPI;";
    }
    [Fact]
    public void Test_AnimalTypesEmptyAtFirst()
    {
      int result = AnimalType.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_ReturnsTrueForSameType()
    {
      AnimalType firstAnimalType = new AnimalType("Dog");
      AnimalType secondAnimalType = new AnimalType("Dog");

      Assert.Equal(firstAnimalType, secondAnimalType);
    }

    [Fact]
    public void Test_Save_SaveAnimalTypeToDatabase()
    {
      AnimalType testAnimalType = new AnimalType("Dog");
      testAnimalType.Save();

      List<AnimalType> result = AnimalType.GetAll();
      List<AnimalType> testList = new List<AnimalType>{testAnimalType};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToAnimalTypeObject()
    {
      AnimalType testAnimalType = new AnimalType("Dog");
      testAnimalType.Save();

      AnimalType savedAnimalType = AnimalType.GetAll()[0];

      int result = savedAnimalType.GetId();
      int testId = testAnimalType.GetId();

      Console.WriteLine("result id is:"  + result);
      Console.WriteLine("test id is:"  + testId);
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindAnimalTypeInDatabase()
    {
      AnimalType testAnimalType = new AnimalType("Dog");
      testAnimalType.Save();

      AnimalType foundAnimalType = AnimalType.Find(testAnimalType.GetId());

      Assert.Equal(testAnimalType, foundAnimalType);
    }


    [Fact]
    public void Test_GetAnimal_RetrievesAllAnimalWithAnimalType()
    {
      AnimalType testAnimalType = new AnimalType("Dog");
      testAnimalType.Save();

      DateTime testDate = new DateTime(2015, 3, 2);

      Animal firstAnimal = new Animal("Brian", "male", testDate, "mudd", testAnimalType.GetId());
      firstAnimal.Save();
      Animal secondAnimal = new Animal("Jack", "male", testDate, "half and half", testAnimalType.GetId());
      secondAnimal.Save();

      List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
      List<Animal> resultAnimalList = testAnimalType.GetAnimals();

      Assert.Equal(testAnimalList, resultAnimalList);
    }

    public void Dispose()
    {
      AnimalType.DeleteAll();
      Animal.DeleteAll();
    }
  }
}
