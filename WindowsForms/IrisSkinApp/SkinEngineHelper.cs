using Sunisoft.IrisSkin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisSkinApp
{
    //NuGet添加HiNetCloud.IrisSkin4
    //封装后方便通过皮肤枚举切换皮肤，也方便对SkinEngine属性的封装扩展
    //记得在执行文件目录下添加皮肤文件\Skins\*.ssk（百度下载）
    //需要根据皮肤文件名修改枚举SkinEnum
    //修改SkinEngineHelper.属性  实现更多功能
    public class SkinEngineHelper : SkinEngine
    {
        private static readonly List<string> Skins = new List<string>();
        public SkinEngineHelper()
        {
            try
            {
                var filesList = Directory.GetFiles(Environment.CurrentDirectory + @"\Skins\", "*.ssk");
                filesList.ToList().ForEach(x =>
                {
                    Skins.Add(x);//加载所有皮肤列表路径
                });
            }
            catch (Exception)
            {
                //Skins.Count = 0;
            }
        }
        public void SetSkinFile(SkinEnum skinEnum)
        {
            var skinPath = $@"{Environment.CurrentDirectory}\Skins\{skinEnum}.ssk";
            if (Skins.Contains(skinPath) && skinEnum.ToString() != SkinEnum.NotSkin.ToString())
            {
                base.SkinFile = skinPath;
                base.Active = true;
            }
            else
            {
                base.Active = false;
            }
        }
        public void WriteSkinsListToTxt()
        {
            //将皮肤文件名写到txt文件方便复制写枚举SkinEnum
            string path = Environment.CurrentDirectory + @"\Skins\SkinsList.txt";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                Skins.ForEach(x =>
                {
                    sw.WriteLine(Path.GetFileNameWithoutExtension(x));
                });
            }
        }

    }
    public enum SkinEnum
    {
        NotSkin = 0,//不使用IrisSkin皮肤
        Calmness = 1,
        CalmnessColor1 = 2,
        CalmnessColor2 = 3,
        DeepCyan = 4,
        DeepGreen = 5,
        DeepOrange = 6,
        DiamondBlue = 7,
        DiamondGreen = 8,
        DiamondOlive = 9,
        DiamondPurple = 10,
        DiamondRed = 11,
        Eighteen = 12,
        EighteenColor1 = 13,
        EighteenColor2 = 14,
        Emerald = 15,
        EmeraldColor1 = 16,
        EmeraldColor2 = 17,
        EmeraldColor3 = 18,
        GlassBrown = 19,
        GlassGreen = 20,
        GlassOrange = 21,
        Longhorn = 22,
        MacOS = 23,
        Midsummer = 24,
        MidsummerColor1 = 25,
        MidsummerColor2 = 26,
        MidsummerColor3 = 27,
        mp10 = 28,
        mp10green = 29,
        mp10maroon = 30,
        mp10mulberry = 31,
        mp10pink = 32,
        mp10purple = 33,
        MSN = 34,
        office2007 = 35,
        OneBlue = 36,
        OneCyan = 37,
        OneGreen = 38,
        OneOrange = 39,
        Page = 40,
        PageColor1 = 41,
        PageColor2 = 42,
        RealOne = 43,
        Silver = 44,
        SilverColor1 = 45,
        SilverColor2 = 46,
        SportsBlack = 47,
        SportsBlue = 48,
        SportsCyan = 49,
        SportsGreen = 50,
        SportsOrange = 51,
        SteelBlack = 52,
        SteelBlue = 53,
        vista1 = 54,
        vista1_green = 55,
        Vista2_color1 = 56,
        Vista2_color2 = 57,
        Vista2_color3 = 58,
        Vista2_color4 = 59,
        Vista2_color5 = 60,
        Vista2_color6 = 61,
        Vista2_color7 = 62,
        Warm = 63,
        WarmColor1 = 64,
        WarmColor2 = 65,
        WarmColor3 = 66,
        Wave = 67,
        WaveColor1 = 68,
        WaveColor2 = 69,
        XPBlue = 70,
        XPGreen = 71,
        XPOrange = 72,
        XPSilver = 73,
    }
}
