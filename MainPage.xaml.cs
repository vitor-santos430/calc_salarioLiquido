using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace sal_liquido
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static double salBruto, inss, irpf, total, salLiquido, valeTransporte, salPorHora, valHrsExtra;
        public static int dependentes, horasExtras, percentHExtras;

        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            salBruto = Convert.ToDouble(txtsalBruto.Text);
            horasExtras = Convert.ToInt32(TxtHoraExtra.Text);

            lblSalBruto.Text = salBruto.ToString("R$#,##");

            if (recebeVTransporte.IsToggled)
            {
                valeTransporte = (6 / 100) * salBruto;

                if(salBruto <= 1693.72)
                {
                    inss = (8 / 100) * salBruto;
                }
                else if(salBruto >= 1693.73 || salBruto <= 2822.90)
                {
                    inss = (9 / 100) * salBruto;
                }
                else if(salBruto >= 2822.91 || salBruto <= 5645.80)
                {
                    inss = (11 / 100) * salBruto;
                }
                else
                {
                    inss = 621.04;
                }

                lblInss.Text = inss.ToString("R$#,##");
                irpf = salBruto - inss;
                
                int contador = 0;
                while(contador < dependentes)
                {
                    irpf -= 189.59;
                    contador++;
                }

                lblIrpf.Text = irpf.ToString("R$#,##");

                if(horasExtras != 0)
                {
                    salPorHora = salBruto / 220;

                    int posPercent = PorcenthorasExtras.SelectedIndex;
                    switch (posPercent)
                    {
                        case 0:
                            percentHExtras = 50;
                            valHrsExtra = salPorHora * 1.5;
                            break;
                        case 1:
                            percentHExtras = 70;
                            valHrsExtra = salPorHora * 1.7;
                            break;
                        case 2:
                            percentHExtras = 100;
                            valHrsExtra = salPorHora * 2;
                            break;
                    }
                    valHrsExtra = horasExtras * valHrsExtra;
                    salLiquido = (salBruto - inss - valeTransporte - irpf) + valHrsExtra;
                    lblTotal.Text = salLiquido.ToString("R$#,##");
                }
                else
                {
                    salLiquido = salBruto - inss - valeTransporte - irpf;
                    lblTotal.Text = salLiquido.ToString("R$#,##");
                }

            }
            else
            {
                DisplayAlert("Desligado", "deslig", "OK");
            }
        }

        private void defDependentes_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            numeroDependentes.Text = defDependentes.Value.ToString("0");
            dependentes = Convert.ToInt32(defDependentes.Value);
        }
    }
}
