using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using BibliotecaClases;
using System.Text.RegularExpressions;
using Datos = BibliotecaDatos;


namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class VentanaPrincipal : MetroWindow
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            //dpCreacion.SelectedDate = DateTime.Today;
            //dpTermino.SelectedDate = DateTime.Today;
            //dpInicio.SelectedDate = DateTime.Today;
            //dpFechatermino.SelectedDate = DateTime.Today;
            cbActividadEmpresaListadoClientes.ItemsSource = Enum.GetValues(typeof(ListadoItems.tipoactividad));
            cbTipoEmpresaListadoClientes.ItemsSource = Enum.GetValues(typeof(ListadoItems.tipoempresa));
            cbActividadEmpresa.ItemsSource = Enum.GetValues(typeof(ListadoItems.tipoactividad));
            cbTipoEmpresa.ItemsSource = Enum.GetValues(typeof(ListadoItems.tipoempresa));
            cbTipoEvento.ItemsSource = Enum.GetValues(typeof(ListadoItems.tipoevento));
            cbModalidad.ItemsSource = Enum.GetValues(typeof(ListadoItems.enumCoffeeBreak));
            //cbModalidad.ItemsSource = Enum.GetValues(typeof(ListadoItems.modalidad));
            //txtNumContrato.IsEnabled = false;
            


        }

        List<Clientes> filtro;
        //Clientes almacenados de la BD
        List<Clientes> list = new List<Clientes>();
        //Contratos almacenados de la BD
        List<Contratos> lc = new List<Contratos>();
        

        
        //Contratos agregados por listado
        ContratosCollection misContratos = new ContratosCollection();

        //Limpiar controles de pestaña Adm.Clientes
        private void LimpiarControlesClientes()
        {
            txtRut.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtEmailContacto.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            cbActividadEmpresa.Text = string.Empty;
            cbTipoEmpresa.Text = string.Empty;

        }

        //Limpiar controles de pestaña Adm. contratos
        private void LimpiarControlesContratos()
        {
            txtNumContrato.Text = string.Empty;
            txtObs.Text = string.Empty;
            txtCreacion.Text = string.Empty;
            txtInicio.Text = string.Empty;
            txtFechatermino.Text = string.Empty;
            txtTermino.Text = string.Empty;
            cbTipoEvento.Text = string.Empty;
            txtAsistentes.Text = string.Empty;
            cbModalidad.Text = string.Empty;
            txtRutContratoBuscar.Text = string.Empty;
            txtPersonalAdicional.Text = string.Empty;
        }

        private void LimpiarControlesListadoClientes()
        {
            txtRutListado.Text = string.Empty;
            cbTipoEmpresaListadoClientes.Text = string.Empty;
            cbActividadEmpresaListadoClientes.Text = string.Empty;
        }

        //Metodo para mostrar clientes
        private void MostrarClientes()
        {
            
            using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities()) 
            {
                list = (from d in db.Cliente
                        select new Clientes
                        {
                            Rut = d.RutCliente,
                            Razonsocial = d.RazonSocial,
                            Nombrecontacto = d.NombreContacto,
                            Emailcontacto = d.MailContacto,
                            Direccion = d.Direccion,
                            Telefono = d.Telefono,
                            Tipoactividad = d.IdActividadEmpresa,
                            Tipoempresa = d.IdTipoEmpresa
                        }).ToList();

            }

            dgListadoClientes.ItemsSource = list;
            dgListadoClientes.Items.Refresh();
            dgListadoClientes.IsReadOnly = true;
            
            
        }

        //Metodo para mostrar contratos
        private void MostrarContratos()
        {
            
            using(Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
            {
                lc = (from d in db.Contrato
                        select new Contratos
                        {
                            Numerocontrato = d.Numero,
                            Creacion = d.Creacion,
                            Termino = d.Termino,
                            Rutclientecontrato = d.RutCliente,
                            Modalidad = d.IdModalidad,
                            Tipoevento = d.IdTipoEvento,
                            Fechahorainicio = d.FechaHoraInicio,
                            Fechahoratermino = d.FechaHoraTermino,
                            Asistentes = d.Asistentes,
                            Personaladicional = d.PersonalAdicional,
                            Valortotalcontrato = d.ValorTotalContrato,
                            Observaciones = d.Observaciones

                        }).ToList();
                    
            }

            dgListadoContratos.ItemsSource = lc;
            dgListadoContratos.Items.Refresh();
            dgListadoContratos.IsReadOnly = true;
            
            
        }

        //Botón guardar clientes a la BD
        private async void BtnGuardarClientes_Click(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text == "" || txtRazonSocial.Text == "" || txtNombreContacto.Text == "" || txtEmailContacto.Text == "" ||
                txtDireccion.Text == "" || txtTelefono.Text == "" || cbActividadEmpresa.Text == "" || cbTipoEmpresa.Text == "")
            {
                await this.ShowMessageAsync("Error", "Debe ingresar totos los datos para guardar");
            }
            else
            {
                try
                {
                    using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                    {

                        bool res = validarRut(txtRut.Text);
                        if (res == false)
                        {
                            await this.ShowMessageAsync("Error", "El rut no es valido");
                        }
                        else
                        {
                            var cli = new Datos.Cliente();
                            cli.RutCliente = txtRut.Text;
                            cli.RazonSocial = txtRazonSocial.Text;
                            cli.NombreContacto = txtNombreContacto.Text;
                            cli.MailContacto = txtEmailContacto.Text;
                            cli.Direccion = txtDireccion.Text;
                            cli.Telefono = txtTelefono.Text;
                            cli.IdActividadEmpresa = cbActividadEmpresa.Text;
                            cli.IdTipoEmpresa = cbTipoEmpresa.Text;

                            db.Cliente.Add(cli);
                            db.SaveChanges();

                            await this.ShowMessageAsync("Aviso", string.Format("Cliente agregado con éxito"));
                            LimpiarControlesClientes();
                            MostrarClientes();
                        }


                    }

                }
                catch
                {
                    await this.ShowMessageAsync("Error", "Cliente ya Existe");
                }
            }

        }

        //Botón llamado de listado clientes desde administracion clientes
        private void BtnListadoClientes_Click(object sender, RoutedEventArgs e)
        {
           MainTC.SelectedItem = tabListado;
        }

        //Botón guardar contratos
        private async void BtnGuardarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (txtCreacion.Text == "" || txtTermino.Text == "" || txtInicio.Text == ""||txtFechatermino.Text == ""||
                 txtObs.Text == "" || txtRutContratoBuscar.Text == "" || txtAsistentes.Text == "" || cbModalidad.Text == "" || cbTipoEvento.Text == ""  )
            {
                await this.ShowMessageAsync("Error", "Debe ingresar todos los datos");
            }
            //falta validar todos los campos para que solo dejar agregar cuando estos esten llenos
            else
            {
                try
                {
                    bool rut = validarRut(txtRutContratoBuscar.Text);
                    if (rut == false)
                    {
                        await this.ShowMessageAsync("Error", "El rut no es valido");
                    }
                    else
                    {
                        using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                        {
                            var con = new Datos.Contrato();

                            con.RutCliente = txtRutContratoBuscar.Text;
                            con.Creacion = txtCreacion.Text;
                            con.Termino = txtTermino.Text;
                            con.FechaHoraInicio = txtInicio.Text;
                            con.FechaHoraTermino = txtFechatermino.Text;
                            con.IdModalidad = cbModalidad.Text;

                            Console.WriteLine("TipoEvento: " + cbTipoEvento.Text);
                            Console.WriteLine("Modalidad: "+cbModalidad.Text);
                            int v2 = 0;
                            if (cbTipoEvento.Text.Equals("CoffeeBeak"))
                            {
                                v2 = 10;

                            }else if(cbTipoEvento.Text.Equals("Cocktail"))
                            {
                                v2 = 20;
                            }
                            else if (cbTipoEvento.Text.Equals("Cenas"))
                            {
                                v2 = 30;
                            }
                            Console.WriteLine(cbModalidad.Text);
                            //con.IdTipoEvento = (Int32.TryParse(cbTipoEvento.Text , out v)  ? v : 0);
                            con.IdTipoEvento = v2;
                            con.Asistentes = int.Parse(txtAsistentes.Text);
                            con.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
                            //con.ValorTotalContrato = txtValorTotalContrato.Text;
                            con.Observaciones = txtObs.Text;
                            
                            //Captura fecha y hora actual para ingresarlos como numero de contrato
                            string fc = DateTime.Now.ToString("yyyyMMdd");
                            string hr = DateTime.Now.ToString("HHmm");
                            string str = String.Concat(fc, hr);

                            txtNumContrato.Text = str;
                            con.Numero = str;
                            

                            db.Contrato.Add(con);
                            db.SaveChanges();

                            await this.ShowMessageAsync("Aviso", "Contrato agregado con éxito");
                            LimpiarControlesContratos();
                            MostrarContratos();

                        }
                    }

                }
                catch(Exception a)
                {
                    Console.WriteLine("EXCEPTION " + a);
                    await this.ShowMessageAsync("Error","Contrato ya existe");
                }

            }

        }


        //Botón buscar clientes
        private async void BtnBuscarClientes_Click(object sender, RoutedEventArgs e)
        {
            if(txtRut.Text != string.Empty)
            {
                try
                {
                    using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                    {
                        string oRut = txtRut.Text;
                        var cli = db.Cliente.Find(oRut);

                        txtRazonSocial.Text = cli.RazonSocial;
                        txtDireccion.Text = cli.Direccion;
                        txtEmailContacto.Text = cli.MailContacto;
                        txtNombreContacto.Text = cli.NombreContacto;
                        txtTelefono.Text = cli.Telefono;
                        cbActividadEmpresa.Text = cli.IdActividadEmpresa;
                        cbTipoEmpresa.Text = cli.IdTipoEmpresa;

                        txtRut.IsEnabled = false;

                    }
                }
                catch
                {
                    await this.ShowMessageAsync("Aviso", string.Format("Persona no Encontrada"));
                    
                }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Para Buscar debe ingresar Rut de la persona");
            }
            

        }

        //Botón eliminar clientes
        private async void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                {
                    string oRut = txtRut.Text;
                    var cli = db.Cliente.Find(oRut);

                    db.Cliente.Remove(cli);
                    db.SaveChanges();
                    await this.ShowMessageAsync("Aviso", "Registro fue eliminado exitosamente.");
                }

                MostrarClientes();
                LimpiarControlesClientes();
                txtRut.IsEnabled = true;

            }
            catch
            {
                await this.ShowMessageAsync("Aviso", "Debe Ingresar un Rut para poder eliminar un Registro");
            }

        }

        //Botón Modificar clientes
        private async void BtnModificarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                {
                    //Verificar despues si modifica clientes
                    string oRut = txtRut.Text;
                    var cli = db.Cliente.Find(oRut);

                    
                    cli.RazonSocial = txtRazonSocial.Text;
                    cli.NombreContacto = txtNombreContacto.Text;
                    cli.MailContacto = txtEmailContacto.Text; 
                    cli.Direccion = txtDireccion.Text;
                    cli.Telefono = txtTelefono.Text;
                    cli.IdActividadEmpresa = cbActividadEmpresa.Text;
                    cli.IdTipoEmpresa = cbTipoEmpresa.Text;

                    db.Entry(cli).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    
                    await this.ShowMessageAsync("Aviso", "Registro actualizado");
                }
                MostrarClientes();
                LimpiarControlesClientes();
                txtRut.IsEnabled = true;

            }
            catch
            {
                await this.ShowMessageAsync("Aviso", "Persona no Encontrada, se debe ingresar un rut para poder modificar un registro");
            }
        }

        //Botón listado contratos desde Adm. Contratos
        private void BtnAdmListarContratos_Click(object sender, RoutedEventArgs e)
        {
            MainTC.SelectedItem = tabListadoContratos;
        }

        //Botón buscar contratos
        private async void BtnBuscarContrato_Click(object sender, RoutedEventArgs e)
        {
            //txtNumContrato.IsEnabled = true;

            if (txtNumContrato.Text != string.Empty)
            {
                try
                {
                    using(Datos.OnBreak2Entities db=new Datos.OnBreak2Entities())
                    {
                        var oCon = txtNumContrato.Text;
                        var cr = db.Contrato.Find(oCon);
                        var cli = db.Cliente.Find(oCon);

                        txtNumContrato.Text = cr.Numero;
                        txtCreacion.Text = cr.Creacion;
                        txtInicio.Text = cr.FechaHoraInicio;
                        txtFechatermino.Text = cr.FechaHoraTermino;
                        txtTermino.Text = cr.Termino;
                        string v1 ="";
                        if (cr.IdTipoEvento.Equals(10))
                        {
                            v1 = "CoffeeBreak";

                        }else if (cr.IdTipoEvento.Equals(30))
                        {
                            v1 = "Cenas";
                        }
                        else if(cr.IdTipoEvento.Equals(20))
                        {
                            v1 = "Cocktail";
                        }

                        cbTipoEvento.Text = v1;
                        txtAsistentes.Text = cr.Asistentes.ToString();
                        txtPersonalAdicional.Text = cr.PersonalAdicional.ToString();
                        txtRutContratoBuscar.Text = cr.RutCliente;
                        cbModalidad.Text = cr.IdModalidad;
                        txtObs.Text = cr.Observaciones;
                        

                    }
                    
                }
                catch
                {
                    await this.ShowMessageAsync("Aviso", "No existe registro");
                }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Debe ingresar un Nº de Contrato para buscar");
            }

            
        }


        //Botón listar clientes
        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {

            MostrarClientes();
            
        }

        //Botón Listar contratos
        private void BtnListarContratos_Click(object sender, RoutedEventArgs e)
        {
            MostrarContratos();
        }

        //Botón limpiar formulario contratos
        private void BtnLimpiarControlesContratos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControlesContratos();
        }

        //Botón limpiar formulario clientes
        private void BtnLimpiarControles_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControlesClientes();
            txtRut.IsEnabled = true;
        }


        //Validaciones de TextBoxs para caracteres
        private void txtRut_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9kK --]+").IsMatch(e.Text);
        }

        private void txtRazonSocial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-Z a-z á-é-í-ó-ú Á-É-Í-Ó-Ú ñ Ñ]+").IsMatch(e.Text);
        }

        private void txtNombreContacto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-Z a-z á-é-í-ó-ú Á-É-Í-Ó-Ú ñ Ñ]+").IsMatch(e.Text);
        }

        private void txtEmailContacto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtDireccion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-Z a-z 0-9 á-é-í-ó-ú Á-É-Í-Ó-Ú ñ Ñ]+").IsMatch(e.Text);
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txtNumContracto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txtDireccionContrato_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9 a-z A-Z -- # á-é-í-ó-ú Á-É-Í-Ó-Ú ñ Ñ]+").IsMatch(e.Text);
        }

        private void txtRutContratoBuscar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9kK --]+").IsMatch(e.Text);
        }



        //Metodo para filtrar                 
        private void FiltrarClientes()
        {
            filtro = list.FindAll(
                delegate (Clientes c)
                {
                    bool tipoempresa = true;
                    bool tipoactividad = true;
                    bool rut = true;

                    if (Convert.ToString(cbTipoEmpresaListadoClientes.SelectedItem) != string.Empty)
                        tipoempresa = Convert.ToString(cbTipoEmpresaListadoClientes.SelectedItem) == c.Tipoempresa;
                    if (Convert.ToString(cbActividadEmpresaListadoClientes.SelectedItem) != string.Empty)
                        tipoactividad = Convert.ToString(cbActividadEmpresaListadoClientes.SelectedItem) == c.Tipoactividad;
                    if (txtRutListado.Text != string.Empty)
                        rut = c.Rut.StartsWith(txtRutListado.Text);

                    return tipoempresa && tipoactividad && rut;
                });
            dgListadoClientes.ItemsSource = filtro;
                        
        }

        //Acciones de los combobox y txt de la ventana Listado de clientes
        private void TxtRutListado_SelectionChanged(object sender, RoutedEventArgs e)
        {
            FiltrarClientes();
        }

        private void CbActividadEmpresaListadoClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarClientes();            
        }

        private void CbTipoEmpresaListadoCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarClientes();
        }


        //Metodo para formatear Rut
        public string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        //format = "." + format; Esto coloca puntos en el rut (No necesarios ahora)
                        cont = 0;
                    }
                }
                return format;
            }
        }


        //Da Formato al txtRut con el metodo antes creado 
        private void ValidarRut_LostFocus(object sender, RoutedEventArgs e)
        {
            txtRut.Text = formatearRut(txtRut.Text);
            
        }

        private void ValidarRutContrato_LostFocus(object sender, RoutedEventArgs e)
        {
            txtRutContratoBuscar.Text = formatearRut(txtRutContratoBuscar.Text);
        }

        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        

        //Limpiar controles de listado cliente
        private void BtnLimpiarControlesListarClientes_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControlesListadoClientes();
        }


        private void CbTipoEvento_DropDownClosed(object sender, EventArgs e)
        {
            Console.WriteLine("TipoEvento: " + cbTipoEvento.Text);

            if (cbTipoEvento.Text.Equals("Cenas"))
            {
                cbModalidad.ItemsSource = Enum.GetValues(typeof(ListadoItems.enumCenas));

            }
            else if (cbTipoEvento.Text.Equals("Cocktail"))
            {
                cbModalidad.ItemsSource = Enum.GetValues(typeof(ListadoItems.enumCocktail));
            }
            else if (cbTipoEvento.Text.Equals("CoffeeBreak"))
            {
                cbModalidad.ItemsSource = Enum.GetValues(typeof(ListadoItems.enumCoffeeBreak));
            }
        }


        //Buscar contrato presionando Enter
        private async void TxtNumContrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtNumContrato.Text != string.Empty)
                {
                    try
                    {
                        using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                        {
                            var oCon = txtNumContrato.Text;
                            var cr = db.Contrato.Find(oCon);
                            //var cli = db.Cliente.Find(oCon);

                            txtNumContrato.Text = cr.Numero;
                            txtCreacion.Text = cr.Creacion;
                            txtInicio.Text = cr.FechaHoraInicio;
                            txtFechatermino.Text = cr.FechaHoraTermino;
                            txtTermino.Text = cr.Termino;
                            string v1 = "";
                            if (cr.IdTipoEvento.Equals(10))
                            {
                                v1 = "CoffeeBreak";

                            }
                            else if (cr.IdTipoEvento.Equals(30))
                            {
                                v1 = "Cenas";
                            }
                            else if (cr.IdTipoEvento.Equals(20))
                            {
                                v1 = "Cocktail";
                            }

                            cbTipoEvento.Text = v1;
                            txtAsistentes.Text = cr.Asistentes.ToString();
                            txtPersonalAdicional.Text = cr.PersonalAdicional.ToString();
                            txtRutContratoBuscar.Text = cr.RutCliente;
                            cbModalidad.Text = cr.IdModalidad;
                            txtObs.Text = cr.Observaciones;


                        }

                    }
                    catch
                    {
                        await this.ShowMessageAsync("Aviso", "No existe registro");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Debe ingresar un Nº de Contrato para buscar");
                }

            }
        }

        //Buscar cliente presionando Enter
        private async void TxtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtRut.Text != string.Empty)
                {
                    try
                    {
                        using (Datos.OnBreak2Entities db = new Datos.OnBreak2Entities())
                        {
                            string oRut = txtRut.Text;
                            var cli = db.Cliente.Find(oRut);

                            txtRazonSocial.Text = cli.RazonSocial;
                            txtDireccion.Text = cli.Direccion;
                            txtEmailContacto.Text = cli.MailContacto;
                            txtNombreContacto.Text = cli.NombreContacto;
                            txtTelefono.Text = cli.Telefono;
                            cbActividadEmpresa.Text = cli.IdActividadEmpresa;
                            cbTipoEmpresa.Text = cli.IdTipoEmpresa;

                            txtRut.IsEnabled = false;

                        }
                    }
                    catch
                    {
                        await this.ShowMessageAsync("Aviso", string.Format("Persona no Encontrada"));

                    }
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Para Buscar debe ingresar Rut de la persona");
                }
            }
        }

        
    }
}