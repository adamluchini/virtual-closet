using System.Collections.Generic;
using System;
using Nancy;
using Closet.Objects;

namespace Closet
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Post["/results"] = _ => {
        Tshirt newTshirt = new Tshirt(Request.Form["color"]);
        newTshirt.Save();

        List<Tshirt> listOfShirts = Tshirt.GetAll();
        return View["results.cshtml", listOfShirts];
      };
      Get["/results"] = _ => {
        List<Tshirt> listOfShirts = Tshirt.GetAll();
        return View["results.cshtml", listOfShirts];
      };
      Get["/clear"] = _ => {
        Tshirt.DeleteAll();
        return View["clear.cshtml"];
      };
      Get["/find"] = _ => {
        return View["find.cshtml"];
      };
      Post["/found"] = _ => {
        int shirtId = int.Parse(Request.Form["id"]);
        Tshirt foundTshirt = Tshirt.Find(shirtId);
        return View["foundshirts.cshtml", foundTshirt];
      };
    }
  }
}
