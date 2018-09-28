using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;

namespace Codes
{
    class HightLevelNetworkControll
    {
        public static string Google_DNS = "8.8.8.8";

        public static List<DNSInfo> DNSList = new List<DNSInfo>();

        public static NetworkManagement manager = new NetworkManagement();

        public static List<string> Unabletoslove = new List<string>();

        public static event EventHandler WriteThing = null;


        public System.Net.IPAddress[] getIPs()
        {
            return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
        }

        public static string ResolvedData()
        {
            string[] Hosts = Adress_need.Address.Split('%');
            string data = string.Empty;
            foreach (var item in Hosts)
            {
                string buff = string.Empty;
                try
                {
                    // phân giải được thì chép nó vào data
                    buff = Dns.GetHostAddresses(item)[0].ToString() + " " + item;
                    data += buff + Environment.NewLine;
                    if (WriteThing!=null)
                    {
                        WriteThing(buff, new EventArgs());
                    }
                    
                }
                catch
                {// không phân giải được thì đưa vào danh sách địa chỉ không thể phân giải
                    HightLevelNetworkControll.Unabletoslove.Add(item);
                    if (WriteThing!=null)
                    {
                        WriteThing("Unable To Solved : " + item, new EventArgs());
                    }
                }
            }
            return data;
        }

        public static bool isAutomaticDNS(string NICID)
        {
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey("SYSTEM");
            key = key.OpenSubKey("CurrentControlSet");
            key = key.OpenSubKey("Services");
            key = key.OpenSubKey("Tcpip");
            key = key.OpenSubKey("Parameters");
            key = key.OpenSubKey("Interfaces");
            key = key.OpenSubKey(NICID);
            if (string.IsNullOrEmpty((string)key.GetValue("NameServer")))
            {
                return true;
            }
            return false;
        }

        

        public static void setGoogleDNS()
        {
            
            if (WriteThing!=null)
            {
                WriteThing("Setting all active NIC's DNS to google's DNS", new EventArgs());
            }
            try
            {       
                NetworkInterface[] Ninterfaces = NetworkInterface.GetAllNetworkInterfaces();
                
                foreach (var item in Ninterfaces)
                {
                    try
                    {
                        // sao lưu lại DNS đang dùng để sau này khôi phục lại
                        string address = item.GetIPProperties().DnsAddresses[0].ToString();

                        // đoạn này cần kiểm tra lại xem khi apter ko có address thì nó sẽ ntn?
                        DNSList.Add(new DNSInfo(item.Description, address,item.Id));
                        manager.setDNS(item.Description, Google_DNS);
                    }
                    catch { }
                }
            }
            catch
            {
                if (WriteThing!=null)
                {
                    WriteThing("An error occur while setting all active NIC's DNS to google's DNS", new EventArgs());
                }
                throw new Exception();
            }
        }

        public static void RestoreDNS()
        {
            foreach (var item in DNSList)
            {
                if (item.isAutoDNS)
                {
                    manager.SetAutoDNS(item.Adapter_name);
                }
                else
                {
                    manager.setDNS(item.Adapter_name, item.Dns_Address);
                }
            }
        }



        public class DNSInfo
        {
            public string Adapter_name;
            public string Dns_Address;
            public string ID;
            public bool isAutoDNS;
            public DNSInfo(string name, string address, string id)
            {
                Adapter_name = name;
                Dns_Address = address;
                ID = id;
                isAutoDNS = HightLevelNetworkControll.isAutomaticDNS(ID);
            }
        }
    }
}
