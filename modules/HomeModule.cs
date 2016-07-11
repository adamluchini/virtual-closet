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
        Tshirt testTshirt = new Tshirt(Request.Form["color"]);
        testTshirt.Save();

        Tshirt foundTshirt = Tshirt.Find(testTshirt.GetId());
        return View["results.cshtml", testTshirt.GetColor()];
      };
    }
  }
}
