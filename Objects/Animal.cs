using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class Animal
  {
    private int Id;
    private string Name;
    private string Gender;
    private DateTime DateOfAdmittance;
    private string Breed;
    private int AnimalTypeId;

    public Animal(string name, string gender, DateTime date, string breed, int animalTypeId, int id = 0)
    {
      Id = id;
      Name = name;
      Gender = gender;
      DateOfAdmittance = date;
      Breed = breed;
      AnimalTypeId = animalTypeId;
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if(!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = this.GetId() == newAnimal.GetId();
        bool nameEquality = this.GetName() == newAnimal.GetName();
        bool genderEquality = this.GetGender() == newAnimal.GetGender();
        bool dateEquality = this.GetDate() == newAnimal.GetDate();
        bool breedEquality = this.GetBreed() == newAnimal.GetBreed();
        bool typeEquality = this.GetAnimalType() == newAnimal.GetAnimalType();

        return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality && typeEquality);
      }
    }

    public int GetId()
    {
      return Id;
    }
    public string GetName()
    {
      return Name;
    }
    public string GetGender()
    {
      return Gender;
    }
    public DateTime GetDate()
    {
      return DateOfAdmittance;
    }
    public string GetBreed()
    {
      return Breed;
    }
    public int GetAnimalType()
    {
      return AnimalTypeId;
    }

    public void SetId(int id)
    {
      Id = id;
    }
    public void SetName(string name)
    {
      Name = name;
    }
    public void SetGender(string gender)
    {
      Gender = gender;
    }
    public void SetDate(DateTime date)
    {
      DateOfAdmittance = date;
    }
    public void SetBreed(string breed)
    {
      Breed = breed;
    }
    public void SetAnimalTypeId(int typeId)
    {
      AnimalTypeId = typeId;
    }

    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals ORDER BY dateOfAdmittance DESC;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        DateTime date = rdr.GetDateTime(3);
        string animalBreed = rdr.GetString(4);
        int typeId = rdr.GetInt32(5);

        Animal newAnimal = new Animal(animalName, animalGender, date, animalBreed, typeId, animalId);
        allAnimals.Add(newAnimal);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allAnimals;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd= new SqlCommand("INSERT INTO animals (name, gender, dateOfAdmittance, breed, animal_type_id) OUTPUT INSERTED.id VALUES(@AnimalName, @AnimalGender, @DateOfAdmittance, @AnimalBreed, @AnimalTypeId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();

      SqlParameter genderParameter = new SqlParameter();
      genderParameter.ParameterName = "@AnimalGender";
      genderParameter.Value = this.GetGender();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@DateOfAdmittance";
      dateParameter.Value = this.GetDate();

      SqlParameter breedParameter = new SqlParameter();
      breedParameter.ParameterName = "@AnimalBreed";
      breedParameter.Value = this.GetBreed();

      SqlParameter typeIdParameter = new SqlParameter();
      typeIdParameter.ParameterName = "@AnimalTypeId";
      typeIdParameter.Value = this.GetAnimalType();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(breedParameter);
      cmd.Parameters.Add(typeIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE id = @AnimalId;", conn);
      SqlParameter animalIdParameter = new SqlParameter();
      animalIdParameter.ParameterName = "@AnimalId";
      animalIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalIdParameter);
      rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalName = null;
      string foundAnimalGender = null;
      DateTime foundDate = new DateTime();
      string foundAnimalBreed = null;
      int foundAnimalTypeId = 0;

      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundDate = rdr.GetDateTime(3);
        foundAnimalBreed = rdr.GetString(4);
        foundAnimalTypeId = rdr.GetInt32(5);
      }
      Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundDate, foundAnimalBreed, foundAnimalTypeId, foundAnimalId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundAnimal;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd= new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
