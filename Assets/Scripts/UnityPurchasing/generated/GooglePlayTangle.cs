// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("waYqCNbrMDlq7p3jPSO5dNN4l0HN89YxGgIsWcCBr/AMf0f2Mxr33DF5IXUPH+d06AHKYiyck8rw6ui/6Gtlalroa2Bo6Gtrau9KMdNU2NlIu9rrvs6JkT5OV2S9g/J4zvGChKnN65kIfHmsmI2eBYGgwKFI8hRdRtmaRvVbre5r+G1uOQfJ3JkP8YPC9W6bwj21tVso3rTBW9bmTlcKTmGmBYjE/0zH9kGh3edpRG+GEGYqJf9jlU6T2sWiEer8YH137Qn7xuDntHG7TakBCKaL1lNZOLkcrZU85Froa0haZ2xjQOwi7J1na2trb2ppiBA2YD6He0eN0BUEa/fxWjimiGEIk0lj9bE40nlgk81PrBHH+ESUQtHPBtpOyMSFYWhpa2pr");
        private static int[] order = new int[] { 7,9,4,9,4,5,9,9,8,11,13,11,12,13,14 };
        private static int key = 106;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
