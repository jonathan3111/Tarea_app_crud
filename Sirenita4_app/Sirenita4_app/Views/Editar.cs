using Sirenita4_app.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Sirenita4_app.Views
{
    public class Editar : ContentPage
    {
        private ListView _listView;
        private Entry _identry;
        private Entry _nombreEntry;
        private Entry _apellidoEntry;
        private Entry _correoEntry;
        private Entry _telefonoEntry;
        private Entry _cantidadEntry;
        private Entry _horaEntry;
        private Button _actualizarButton;

        registro _registro = new registro();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");
        public Editar()
        {
            this.Title = "Editar";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<registro>().OrderBy(x => x.nombre).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _identry = new Entry();
            _identry.Placeholder = "ID";
            _identry.IsVisible = false;
            stackLayout.Children.Add( _identry );

            _nombreEntry = new Entry();
            _nombreEntry.Keyboard = Keyboard.Text;
            _nombreEntry.Placeholder = "Nombre";
            stackLayout.Children.Add(_nombreEntry);

            _apellidoEntry = new Entry();
            _apellidoEntry.Keyboard = Keyboard.Text;
            _apellidoEntry.Placeholder = "Apellidos";
            stackLayout.Children.Add(_apellidoEntry);

            _correoEntry = new Entry();
            _correoEntry.Keyboard = Keyboard.Text;
            _correoEntry.Placeholder = "correo";
            stackLayout.Children.Add(_correoEntry);

            _telefonoEntry = new Entry();
            _telefonoEntry.Keyboard = Keyboard.Text;
            _telefonoEntry.Placeholder = "telefono";
            stackLayout.Children.Add(_telefonoEntry);

            _cantidadEntry = new Entry();
            _cantidadEntry.Keyboard = Keyboard.Text;
            _cantidadEntry.Placeholder = "cantidad de personas";
            stackLayout.Children.Add(_cantidadEntry);

            _horaEntry = new Entry();
            _horaEntry.Keyboard = Keyboard.Text;
            _horaEntry.Placeholder = "hora";
            stackLayout.Children.Add(_horaEntry);

            _actualizarButton = new Button();
            _actualizarButton.Text = "Actualizar";
            _actualizarButton.Clicked += _actualizarButton_Clicked;
            stackLayout.Children.Add(_actualizarButton);

            Content = stackLayout;

        }

        private async void _actualizarButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            registro registro = new registro()
            {
                Id = Convert.ToInt32(_identry.Text),
                nombre = _nombreEntry.Text,
                Apellidos = _apellidoEntry.Text,
                correo = _correoEntry.Text,
                telefono = Convert.ToInt32 (_telefonoEntry.Text),
                cantidad_p = Convert.ToInt32(_cantidadEntry.Text),
                hora = _horaEntry.Text
            };
            db.Update(registro);
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _registro = (registro)e.SelectedItem;
            _identry.Text = _registro.Id.ToString();
            _nombreEntry.Text = _registro.nombre;
            _apellidoEntry.Text = _registro.Apellidos;
            _correoEntry.Text = _registro.correo;
            _telefonoEntry.Text = _registro.telefono.ToString();
            _cantidadEntry.Text = _registro.cantidad_p.ToString();
            _horaEntry.Text = _registro.hora;
        }
    }
}