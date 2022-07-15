using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addon.TCKNCheck.TcknService;

namespace Addon.TCKNCheck
{
    public class TCKNCheck
    {

        public static Dictionary<string, object> Validate(List<Dictionary<string, object>> parameters)
        {
            // TCKN servis adresi.
            var webServiceUrl = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";

            var result = new Dictionary<string, object>();

            var List = new List<Dictionary<string, object>>();

            result["Error"] = "";

            try

            {
                // Bu parametreler XPODA dan gelecek. Asagidaki linkteki gibi gonderebilirsiniz.
                // https://docs.xpoda.com/hc/tr/articles/360016637520-Xpoda-Platformu-i%C3%A7in-Addon-Geli%C5%9Ftirme
                //
                var name = parameters[0]["ad"].ToString();
                var surname = parameters[1]["soyad"].ToString();
                var birthyear = parameters[2]["yil"].ToString();
                var tckn = parameters[3]["tckn"].ToString();

                var isValid = false;

                var remoteAddress = new System.ServiceModel.EndpointAddress(webServiceUrl);

                using (var nService = new KPSPublicSoapClient(new System.ServiceModel.BasicHttpsBinding(), remoteAddress))
                {
                    //set timeout
                    nService.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 0, 1000);

                    //call web service method
                    var response = nService.TCKimlikNoDogrula(new TCKimlikNoDogrulaRequest
                    {
                        Body = new TCKimlikNoDogrulaRequestBody
                        {
                            TCKimlikNo = Int64.Parse(tckn),
                            Ad = name,
                            Soyad = surname,
                            DogumYili = Int32.Parse(birthyear)
                        }
                    });
                    
                    // True veya False doner
                    isValid = response.Body.TCKimlikNoDogrulaResult;
                    
                }

                // Result indexi ile bir dict icine ekleyip yaniti donuyoruz.
                List.Add(new Dictionary<string, object> { { "Result", isValid } });

                result["List"] = List;

            }

            catch (Exception ex)

            {

                result["Error"] = ex.Message;

            }

            return result;


        }
    }
}
