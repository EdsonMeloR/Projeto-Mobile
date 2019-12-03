using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Projeto_Mobile.Class;

namespace Projeto_Mobile
{
    class EntregasAdapter : BaseAdapter<Carga>
    {
        List<Carga> listaC;
        Activity context;

        public EntregasAdapter(Activity context, List<Carga> lista):base()
        {
            this.listaC = lista;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override Carga this[int position]
        {
            get { return listaC[position]; }
        }
        public override int Count
        {
            get { return listaC.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null)
                v = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            v.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "Id Carga: " + listaC[position].Id;
            v.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "Nome Carga: " + listaC[position].NomeProduto;
            return v;
        }
    }
}