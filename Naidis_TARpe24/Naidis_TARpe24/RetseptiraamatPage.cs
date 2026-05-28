using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24;

public class RetseptiraamatPage : TabbedPage
{
    public RetseptiraamatPage()
    {
        Title = "Retseptiraamat";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        var uusLeht = new UusRetseptLeht();
        var nimekirjaLeht = new MinuRetseptidLeht();

        uusLeht.RetseptSalvestatud += () => nimekirjaLeht.LaeRetseptid();

        Children.Add(uusLeht);
        Children.Add(nimekirjaLeht);
    }
}
