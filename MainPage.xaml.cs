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

        public static double salBruto , inss, irpf, total, salLiquido, valeTransporte, salPorHora, valHrsExtra;
        public static int dependentes, horasExtras, percentHExtras;

        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            salBruto = Convert.ToDouble(txtsalBruto.Text);
            horasExtras = Convert.ToInt32(TxtHoraExtra.Text);
            lblSalBruto.Text = salBruto.ToString("R$#,##");

            if (recebeVTransporte.IsToggled)
            {
                valeTransporte = 0.06 * salBruto;

                Calcule();

                if (horasExtras != 0)
                {
                    salPorHora = salBruto / 220f;

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

                Calcule();

                if (horasExtras != 0)
                {
                    salPorHora = salBruto / 220f;

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
                    salLiquido = (salBruto - inss - irpf) + valHrsExtra;
                    lblTotal.Text = salLiquido.ToString("R$#,##");
                }
                else
                {
                    salLiquido = salBruto - inss - irpf;
                    lblTotal.Text = salLiquido.ToString("R$#,##");
                }
            }
        }

        private void defDependentes_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            numeroDependentes.Text = defDependentes.Value.ToString("0");
            dependentes = Convert.ToInt32(defDependentes.Value);
        }

        public void Calcule()
        {
            if (salBruto <= 1693.72)
            {
                inss = 0.08 * salBruto;
            }
            else if (salBruto >= 1693.73 && salBruto <= 2822.90)
            {
                inss = 0.09 * salBruto;
            }
            else if (salBruto >= 2822.91 && salBruto <= 5645.80)
            {
                inss = 0.11 * salBruto;
            }
            else
            {
                inss = 621.04;
            }

            lblInss.Text = inss.ToString("R$#,##");

            irpf = salBruto - inss;

            if (irpf <= 1903.98)
            {
                irpf = 0f;
            }
            else if (irpf >= 1903.99 && irpf <= 2836.65)
            {
                irpf = (7.5 / 100f) * irpf;
                irpf -= 142.80;
            }
            else if (irpf >= 2626.66 && irpf <= 3751.05)
            {
                irpf = (15f / 100f) * irpf;
                irpf -= 354.80;
            }
            else if (irpf >= 3751.06 && irpf <= 4664.68)
            {
                irpf = (22.5 / 100f) * irpf;
                irpf -= 636.13;
            }
            else if (irpf >= 4664.49)
            {
                irpf = (27.5 / 100f) * irpf;
                irpf -= 869.36;
            }

            int contador = 0;
            while (contador < dependentes)
            {
                irpf -= 189.59;
                contador++;
            }

            lblIrpf.Text = irpf.ToString("R$#,##");
        }
    }
}
