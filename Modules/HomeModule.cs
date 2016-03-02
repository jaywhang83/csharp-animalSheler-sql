using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace AnimalShelter
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<AnimalType> allAnimalTypes = AnimalType.GetAll();
        return View["index.cshtml", allAnimalTypes];
      };
      Get["/animals"] = _ =>
      {
        List<Animal> allAnimals = Animal.GetAll();
        return View["animals.cshtml", allAnimals];
      };
      Get["/animalTypes/new"] = _ =>
      {
        return View["animalTypes_form.cshtml"];
      };
      Post["/animalTypes/new"] = _ =>
      {
        AnimalType newAnimalType = new AnimalType(Request.Form["animal-type"]);
        newAnimalType.Save();
        return View["success.cshtml"];
      };
      Get["/animals/new"] = _ =>
      {
        List<AnimalType> allAnimalTypes = AnimalType.GetAll();
        return View["animals_form.cshtml", allAnimalTypes];
      };
      Post["/animals/new"] = _ =>
      {
        DateTime newDate = DateTime.Parse(Request.Form["date"]);
        Animal newAnimal = new Animal(Request.Form["animal-name"], Request.Form["animal-gender"], newDate, Request.Form["animal-breed"], Request.Form["animalType-id"]);
        newAnimal.Save();
        return View["success.cshtml"];
      };
      Post["/animals/delete"] = _ =>
      {
        Animal.DeleteAll();
        return View["type_deleted.cshtml"];
      };
      Get["/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedAnimalType = AnimalType.Find(parameters.id);
        var AnimalTypeAnimals = selectedAnimalType.GetAnimals();
        model.Add("animalType", selectedAnimalType);
        model.Add("animals", AnimalTypeAnimals);
        return View["animalType.cshtml", model];
      };
      Post["/animalTypes/delete"] = _ =>
      {
        AnimalType.DeleteAll();
        return View["type_deleted.cshtml"];
      };
      Get["/animals/breed"] = _ =>
      {
        List<Animal> allAnimals = Animal.GetAllByBreed();
        return View["breed.cshtml", allAnimals];
      };
      Get["/animals/alphabetical"] = _ =>
      {
        List<Animal> allAnimals = Animal.GetByAlphabeticalOrder();
        return View["alphabetical.cshtml", allAnimals];
      };
    }
  }
}
