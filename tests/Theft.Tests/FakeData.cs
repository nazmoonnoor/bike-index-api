using System;
using System.Collections.Generic;
using Theft.Service.Models;

namespace Theft.Tests;

public static class FakeData
{
    public static readonly BikeTheftResponse BikeTheftFakes = new BikeTheftResponse(
        new List<Bike>
        {
            new Bike("description 1", new List<string>(){ "blue" }, "model", 1, string.Empty, "WBK73220IF", "stolen", true, new List<double>(), "Amsterdam", string.Empty, "title", ""),
            new Bike("description 2", new List<string>(){ "blue" }, "model", 1, string.Empty, "WBK73220IE", "stolen", true, new List<double>(), "Amsterdam", string.Empty, "title", ""),
            new Bike("description 3", new List<string>(){ "blue" }, "model", 1, string.Empty, "WBK73220ID", "stolen", true, new List<double>(), "Amsterdam", string.Empty, "title", "")
        });

    public static readonly string BikeTheftJson = @"{
          'bikes': [
            {
              'date_stolen': 1647734259,
              'description': null,
              'frame_colors': [
                'Blue'
              ],
              'frame_model': 'xcaliber',
              'id': 1250914,
              'is_stock_img': false,
              'large_img': null,
              'location_found': null,
              'manufacturer_name': 'Trek',
              'external_id': null,
              'registry_name': null,
              'registry_url': null,
              'serial': '123',
              'status': 'stolen',
              'stolen': true,
              'stolen_coordinates': [
                52.52,
                13.4
              ],
              'stolen_location': 'Berlin, DE',
              'thumb': null,
              'title': '2022 Trek xcaliber',
              'url': 'https://bikeindex.org/bikes/1250914',
              'year': 2022
            }
          ]
        }";
}
