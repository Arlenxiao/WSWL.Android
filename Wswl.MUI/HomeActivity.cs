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

namespace Wswl.MUI
{
    [Activity(Label = "��ҳ")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Home);

            //��ʼ���¼�
            InitEvent();
        }

        /// <summary>��ʼ���¼�</summary>
        private void InitEvent()
        {
            //���ز˵�
            FindViewById<ImageView>(Resource.Id.home_menu_gateway).Click += (s, e) => { Toast.MakeText(this, "���ز˵�", ToastLength.Short).Show(); };

            //������ʾ
            FindViewById<TextView>(Resource.Id.lbl_home_alarm).Click += (s, e) => { Toast.MakeText(this, "������ʾ--36", ToastLength.Long).Show(); };

            //���ð�ť
            FindViewById<ImageView>(Resource.Id.img_home_icon_setting).Click += (s, e) => { Toast.MakeText(this, "���ù���", ToastLength.Short).Show(); };

            //������Ϣ
            FindViewById<TextView>(Resource.Id.lbl_home_warning).Click += (s, e) => { Toast.MakeText(this, "������Ϣ--�Ȳ����Ŵ��ű���", ToastLength.Short).Show(); };

            //�����豸ͳ��
            FindViewById<Button>(Resource.Id.btn_home_online_devices).Click += (s, e) => { Toast.MakeText(this, "��ǰ�����豸:7/21", ToastLength.Short).Show(); };

            //�����û�ͳ��
            FindViewById<Button>(Resource.Id.btn_home_online_user).Click += (s, e) => { Toast.MakeText(this, "��ǰ�����豸�û�:1/6", ToastLength.Short).Show(); };

            //W0103
            FindViewById<Button>(Resource.Id.btn_home_dev_w0103_01).Click += (s, e) => { Toast.MakeText(this, "����W0103,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //���ҿ���
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_01).Click += (s, e) => { Toast.MakeText(this, "���ҿ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //��������
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_01).Click += (s, e) => { Toast.MakeText(this, "��������,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�칫�ҿ���
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_02).Click += (s, e) => { Toast.MakeText(this, "�칫�ҿ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�������
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_02).Click += (s, e) => { Toast.MakeText(this, "�������,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�����Ŵ���
            FindViewById<Button>(Resource.Id.btn_home_dev_A0501_01).Click += (s, e) => { Toast.MakeText(this, "�����Ŵ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };
        }
    }
}