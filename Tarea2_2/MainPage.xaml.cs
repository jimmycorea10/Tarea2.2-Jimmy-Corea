﻿using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Tarea2_2.Models;



namespace Tarea2_2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            
            var path = System.IO.Path.Combine("", "FirmaTest.jpg");

            Stream image_= await   padView.GetImageStreamAsync(SignatureImageFormat.Jpeg);
            BinaryReader br = new BinaryReader(image_);
            br.BaseStream.Position = 0;
            Byte[] All = br.ReadBytes((int)image_.Length);


            if ( String.IsNullOrWhiteSpace(txtDescripcion.Text) || String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                await DisplayAlert("Error", "No completó todos los campos", "OK");
            }
            else
            {
                var emple = new constructor
                {
                    nombre = txtNombre.Text,
                    descripcion = txtDescripcion.Text,
                    imgM = (byte[])All
                    /*
                    byte[] image = (byte[])All;
                    ImageSource imageSource = null;
                    imageSource = ImageSource.FromStream(() => new MemoryStream(image));
                    imgFoto.Source = imageSource;
                    */
            };
                var resultado = await App.BaseDatos.EmpleadoGuardar(emple);
                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "Datos Guardados!!", "OK");
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    padView.Clear();



                }
                else
                {
                    await DisplayAlert("Aviso", "Error!!", "OK");
                }
            }
                
                //display the signature
                
               
                
        
        }
      
        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            var newpage = new lista();
            await Navigation.PushAsync(newpage);
        }
    }
}
