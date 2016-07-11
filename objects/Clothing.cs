using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Closet.Objects
{
  public class Tshirt
  {
    private int _id;
    private string _color;

    public Tshirt(string Color, int Id = 0)
    {
      _id = Id;
      _color = Color;
    }

    // public Pants(string Color, int Id = 0)
    // {
    //   _id = Id;
    //   _color = Color;
    // }
    //
    // public Socks(string Color, int Id = 0)
    // {
    //   _id = Id;
    //   _color = Color;
    // }

    public override bool Equals(System.Object otherTshirt)
    {
      if (!(otherTshirt is Tshirt))
      {
        return false;
      }
      else
      {
        Tshirt newTshirt = (Tshirt) otherTshirt;
        bool idEquality = (this.GetId() == newTshirt.GetId());
        bool colorEquality = (this.GetColor() == newTshirt.GetColor());
        return (idEquality && colorEquality);
      }
    }


    public int GetId()
    {
      return _id;
    }
    public string GetColor()
    {
      return _color;
    }
    public void SetColor(string newColor)
    {
      _color = newColor;
    }

    public static List<Tshirt> GetAll()
    {
      List<Tshirt> allTshirts = new List<Tshirt>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id, color FROM tshirts;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int tshirtId = rdr.GetInt32(0);
        string tshirtColor = rdr.GetString(1);
        Tshirt newTshirt = new Tshirt(tshirtColor, tshirtId);
        allTshirts.Add(newTshirt);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTshirts;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tshirts (color) OUTPUT INSERTED.id VALUES (@TshirtColor);", conn);

      SqlParameter colorParameter = new SqlParameter();
      colorParameter.ParameterName = "@TshirtColor";
      colorParameter.Value = this.GetColor();
      cmd.Parameters.Add(colorParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tshirts;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Tshirt Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id, color FROM tshirts WHERE id = @TshirtId;", conn);
      SqlParameter tshirtIdParameter = new SqlParameter();
      tshirtIdParameter.ParameterName = "@TshirtId";
      tshirtIdParameter.Value = id.ToString();
      cmd.Parameters.Add(tshirtIdParameter);
      rdr = cmd.ExecuteReader();

      int foundTshirtId = 0;
      string foundTshirtColor = null;
      while(rdr.Read())
      {
        foundTshirtId = rdr.GetInt32(0);
        foundTshirtColor = rdr.GetString(1);
      }
      Tshirt foundTshirt = new Tshirt(foundTshirtColor, foundTshirtId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundTshirt;
    }

    public override int GetHashCode()
    {
         return this.GetColor().GetHashCode();
    }


  }
}
