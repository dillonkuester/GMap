using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMap
{
    public partial class Form1 : Form
    {
        private List<PointLatLng> _points;

        public Form1()
        {
            InitializeComponent();
            _points = new List<PointLatLng>();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map.ShowCenter = false; 
        }

        private void btnLoadIntoMap_Click(object sender, EventArgs e)
        {
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(txtLat.Text);
            double lng = Convert.ToDouble(txtLong.Text);
            map.Position = new PointLatLng(lat, lng);
            map.MinZoom = 1;
            map.MaxZoom = 100;
            map.Zoom = 10; //current zoom level

            //create custom marker
       //     Bitmap bmpMarker = (Bitmap)Image.FromFile("img/redmarker.png");

            PointLatLng point = new PointLatLng(lat, lng);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
            
            // 1 Create overlay
            GMapOverlay markers = new GMapOverlay("markers");
            // 2 Add all available markers to that overlay
            markers.Markers.Add(marker);
            // 3 Cover map with overlay
            map.Overlays.Add(markers);
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            _points.Add(new PointLatLng(Convert.ToDouble(txtLat.Text),
                Convert.ToDouble(txtLong.Text)));
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            _points.Clear();
        }

        private void btnGetRouteInformation_Click(object sender, EventArgs e)
        {
            //create route
            var route = GoogleMapProvider.Instance
                .GetRoute(_points[0], _points[1], false, false, 14);
            var r = new GMapRoute(route.Points, "My Route")
            {
                Stroke = new Pen(Color.Red, 5)
            };
            //create route overlay it on the map
            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            map.Overlays.Add(routes);
        }
    }
}
