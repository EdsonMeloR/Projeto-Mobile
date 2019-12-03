using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace Projeto_Mobile.Class
{
    public class ItensNotaTransporte
    {
        //Atributos
        private Carga idCarga;
        private NotaTransporte idNotaTransporte;
        Banco db;
        //Propiedades
        public Carga IdCarga { get => idCarga; set => idCarga = value; }
        public NotaTransporte IdNotaTransporte { get => idNotaTransporte; set => idNotaTransporte = value; }
        //Métodos Construtores
        public ItensNotaTransporte(Carga idCarga, NotaTransporte idNotaTransporte)
        {
            this.IdCarga = idCarga;
            this.IdNotaTransporte = idNotaTransporte;
        }
        public ItensNotaTransporte()
        {
            IdCarga = new Carga();
            IdNotaTransporte = new NotaTransporte();
        }
        //Métodos
        public bool InserirItensNotaTransporte(int idcarga, int idnota)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandType =  CommandType.StoredProcedure;
                comm.CommandText = "insert_itensnotatrans";
                comm.Parameters.Add("_idcarga", MySqlDbType.Int32).Value = idcarga;
                comm.Parameters.Add("_idnotatransporte", MySqlDbType.Int32).Value = idnota;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
            finally
            {
                try
                {
                    if (comm != null)
                        comm.Connection.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        /// <summary>
        /// Listando todos os itens do pedidos que já estão associados a uma nota de transporte
        /// </summary>
        public List<ItensNotaTransporte> ListarItensNotaTransportePedido(int idPedido)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            List<ItensNotaTransporte> lista = new List<ItensNotaTransporte>();
            ItensNotaTransporte ints;
            try
            {
                comm.CommandText = "select ints.idCarga, ints.idNotaTransporte from itensnotatransporte as ints left join carga as c on ints.idCarga = c.idCarga where c.idPedidos = " + idPedido;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    ints = new ItensNotaTransporte();
                    ints.IdCarga.Id = dr.GetInt32(0);
                    ints.IdNotaTransporte.Id = dr.GetInt32(1);
                    lista.Add(ints);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
            finally
            {
                try
                {
                    if (comm != null)
                        comm.Connection.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        /// <summary>
        /// Listando Itens De nota de transporte pelo id nota transporte
        /// </summary>        
        public List<ItensNotaTransporte> ListarItensNotaTransporte(int idNota)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            List<ItensNotaTransporte> lista = new List<ItensNotaTransporte>();
            ItensNotaTransporte ints;
            try
            {
                comm.CommandText = "select * from itensnotatransporte where idNotaTransporte = " + idNota;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    ints = new ItensNotaTransporte();
                    ints.IdCarga.Id = dr.GetInt32(0);
                    ints.IdNotaTransporte.Id = dr.GetInt32(1);
                    lista.Add(ints);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
            finally
            {
                try
                {
                    if (comm != null)
                        comm.Connection.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
    }
}
