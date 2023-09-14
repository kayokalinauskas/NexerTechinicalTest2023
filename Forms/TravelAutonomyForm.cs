namespace TesteTecnico2023.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using TesteTecnico2023.Model;
    using static System.Net.Mime.MediaTypeNames;

    public partial class TravelAutonomyForm : Form
    {
        public TravelAutonomyForm()
        {
            InitializeComponent();
        }

        public class FreteInfo
        {
            public string VehicleBrand { get; set; }
            public string VehicleModel { get; set; }
            public double ShippingFee { get; set; }
            public string VehicleType { get; set; }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            List<FreteInfo> shippingList = new List<FreteInfo>();
            foreach (var data in Data.VEHICLES)
            {
                var fueltype = Data.FUEL_PRICES.Find(fuel => fuel.name == data.Fuel);
                double InputCargoWeight = Convert.ToDouble(txtWeight.Text);
                double inputShippingDistance = Convert.ToDouble(txtDistance.Text);
                double VehicleSupportedWeight = Convert.ToDouble(data.WeightSupported);
                double totalDistance = Math.Ceiling(InputCargoWeight / VehicleSupportedWeight) * inputShippingDistance;
                double shippingFee = (totalDistance / data.Autonomy) * fueltype.price;
                shippingList.Add(new FreteInfo { VehicleType = data.Type(),VehicleBrand = data.Brand, VehicleModel = data.ModelName, ShippingFee = shippingFee });

            }

            var cheapestShippingFee = shippingList.OrderBy(frete => frete.ShippingFee).First();
            txtShippingPrice.Text = string.Format(new CultureInfo("pt-BR"), "{0:C2}", cheapestShippingFee.ShippingFee);
            txtVehicleBrand.Text = cheapestShippingFee.VehicleBrand;
            txtVehicleModel.Text = cheapestShippingFee.VehicleModel;
            txtVehicleType.Text = cheapestShippingFee.VehicleType;
        }
    }
}
