using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
  public class AnimalType
  {
    private int Id;
    private string Type;

    public AnimalType(string type, int id = 0)
    {
        Id = id;
        Type = type;
    }
    public override bool Equals(System.Object otherType)
    {
      if(!(otherType is AnimalType))
      {
        return false;
      }
      else
      {
        AnimalType newAnimalType = (AnimalType) otherType;
        bool idEquality = this.GetId() == newAnimalType.GetId();
        bool typeEquality = this.GetAnimalType() == newAnimalType.GetAnimalType();
        return (idEquality && typeEquality);
      }
    }

    public int GetId()
    {
      return Id;
    }

    public string GetAnimalType()
    {
      return Type;
    }

    public void SetId(int id)
    {
      Id = id;
    }

    public void SetType(string type)
    {
      Type = type;
    }

    public static List<AnimalType> GetAll()
    {
      List<AnimalType> allAnimalType = new List<AnimalType>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal_types;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int typeId = rdr.GetInt32(0);
        string animalType = rdr.GetString(1);
        AnimalType  newAnimalType = new AnimalType(animalType, typeId);
        allAnimalType.Add(newAnimalType);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn !=null)
      {
        conn.Close();
      }
      return allAnimalType;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animal_types (type) OUTPUT INSERTED.id VALUES (@animalTypeName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@animalTypeName";
      nameParameter.Value = this.GetAnimalType();
      cmd.Parameters.Add(nameParameter);
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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animal_types;", conn);
      cmd.ExecuteNonQuery();
    }

    public static AnimalType Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal_types WHERE id = @animalTypeId;", conn);
      SqlParameter animalTypeIdParameter = new SqlParameter();
      animalTypeIdParameter.ParameterName = "@animalTypeId";
      animalTypeIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalTypeIdParameter);
      rdr = cmd.ExecuteReader();

      int foundTypeId = 0;
      string foundType = null;

      while(rdr.Read())
      {
        foundTypeId = rdr.GetInt32(0);
        foundType = rdr.GetString(1);
      }
      AnimalType foundAnimalType = new AnimalType(foundType, foundTypeId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundAnimalType;
    }

    public List<Animal> GetAnimals()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE animal_type_id = @animalTypeId;", conn);
      SqlParameter animalTypeIdParameter = new SqlParameter();
      animalTypeIdParameter.ParameterName = "@animalTypeId";
      animalTypeIdParameter.Value = this.GetId();
      cmd.Parameters.Add(animalTypeIdParameter);
      rdr = cmd.ExecuteReader();

      List<Animal> animals = new List<Animal> {};
      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        DateTime dateOfAdmittance = rdr.GetDateTime(3);
        string animalBreed = rdr.GetString(4);
        int animalTypeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, dateOfAdmittance, animalBreed, animalTypeId, animalId);
        animals.Add(newAnimal);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return animals;
    }
  }
}
