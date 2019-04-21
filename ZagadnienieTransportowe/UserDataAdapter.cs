﻿using App.Core;
using App.Core.Model;
using App.Core.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using ZagadnienieTransportowe.Controls;

namespace ZagadnienieTransportowe
{
   internal static class UserDataAdapter
   {
      internal static UserData Adapt(Dictionary<string, LocalizedTextBox> a_cells
                                    ,Dictionary<int, LocalizedTextBox> a_odbiorcy
                                    ,Dictionary<int, LocalizedTextBox> a_dostawcy)
      {
         var us_grid = Utility.CreateEmptyGrid(a_dostawcy.Count(), a_odbiorcy.Count());
         foreach (var cell in a_cells)
         {
            var y = int.Parse(cell.Key[0].ToString());
            var x = int.Parse(cell.Key[1].ToString());

            if (us_grid[y][x].Id != cell.Key)
               throw new Exception($"Podczas przygotowania danych wystąpił bład. Oczekiwane Id '{cell.Key}', pobrane '{us_grid[y][x]?.Id ?? "-"}'");

            us_grid[y][x].KosztyJednostkowe = int.Parse(cell.Value.Text);
         }

         var us_dostawcy = new List<InputData>();
         foreach (var d in a_dostawcy)
         {
            var data = new InputData(d.Key, InputType.Dostawca, int.Parse(d.Value.Text));
            us_dostawcy.Add(data);
         }

         var us_odbiorcy = new List<InputData>();
         foreach (var o in a_odbiorcy)
         {
            var data = new InputData(o.Key, InputType.Odbiorca, int.Parse(o.Value.Text));
            us_odbiorcy.Add(data);
         }

         return new UserData(us_grid, us_dostawcy, us_odbiorcy);
      }
   }
}