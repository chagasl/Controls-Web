using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Drawing;

namespace PlantControl.Views
{
    public partial class ViewStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();
            int assemblyPercentage = 0;
            int machinePercentage = 0;

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "ViewStatus");
            if (authorizated)
            {
                SQLQuery sQLQuery = new SQLQuery();

                assemblyPercentage = sQLQuery.SumAssyLineBackup("ASSEMBLY");
                machinePercentage = sQLQuery.SumAssyLineBackup("MACHINING");

                Highcharts chart = new Highcharts("chart")
               .InitChart(new Chart
               {
                   Type = ChartTypes.Gauge,
                   PlotBackgroundColor = null,
                   PlotBackgroundImage = null,
                   PlotBorderWidth = 0,
                   PlotShadow = false,
                   MarginTop = 20,
                   MarginBottom = 0,
                   MarginLeft = 0,
                   MarginRight = 0,
                   Height = 470,
                   Width = 470,

               })
               .SetTitle(new Title { Text = "BACKUPS USINAGEM" })
               .SetPane(new Pane
               {
                   StartAngle = -150,
                   EndAngle = 150,
                   Background = new[]
                   {
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#FFF" }, { 1, "#333" } }
                            }),
                            BorderWidth = new PercentageOrPixel(2),
                            OuterRadius = new PercentageOrPixel(109, true)
                        },
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#333" }, { 1, "#FFF" } }
                            }),
                            BorderWidth = new PercentageOrPixel(1),
                            OuterRadius = new PercentageOrPixel(107, true)
                        },
                        new BackgroundObject(),
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(Color.LightGray),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(105, true),
                            InnerRadius = new PercentageOrPixel(102, true)
                        }
                   }
               })
               .SetYAxis(new YAxis
               {
                   Min = 0,
                   Max = 100,

                   MinorTickWidth = 1,
                   MinorTickLength = 15,
                   MinorTickPosition = TickPositions.Inside,
                   MinorTickColor = Color.Black,
                   TickWidth = 1,
                   TickPosition = TickPositions.Inside,
                   TickLength = 30,
                   TickInterval = 5,             
                   TickColor = Color.Black,
                   Labels = new YAxisLabels
                   {
                       Step = 2,
                       Distance = -43,
                       Style = "color:'black', fontSize:'15px', fontWeight:'bold'",

                   },
                   Title = new YAxisTitle { Text = "%", Style = "color:'black', fontWeight:'bold', fontSize:'23px'" },
                   PlotBands = new[]
                   {
                        new YAxisPlotBands { From = 0, To = 40, Color = Color.Tomato,  InnerRadius = new PercentageOrPixel(35, true) },
                        new YAxisPlotBands { From = 40, To = 80, Color = Color.Yellow, InnerRadius = new PercentageOrPixel(35, true) },
                        new YAxisPlotBands { From = 80, To = 100, Color = Color.LightGreen, InnerRadius = new PercentageOrPixel(35, true) }
                   }
               })
               .SetPlotOptions(new PlotOptions
                {
                    Gauge = new PlotOptionsGauge
                    {
                        DataLabels = new PlotOptionsGaugeDataLabels
                        {
                            Style = "color:'black', fontSize:'26px', fontWeight:'bold'",
                            VerticalAlign = VerticalAligns.Bottom,
                            Y = 140,

                        }
                    }
                })
               .SetSeries(new Series
               {
                   Name = "Result",
                   Data = new Data(new object[] { machinePercentage }),
               });

                machining.Text = chart.ToHtmlString();


                Highcharts chart1 = new Highcharts("chart1")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Gauge,
                    PlotBackgroundColor = null,
                    PlotBackgroundImage = null,
                    PlotBorderWidth = 0,
                    PlotShadow = false,
                    MarginTop = 20,
                    MarginBottom = 0,
                    MarginLeft = 0,
                    MarginRight = 0,
                    Height = 470,
                    Width = 470,
                })
                .SetTitle(new Title { Text = "BACKUPS MONTAGEM" })
                .SetPane(new Pane
                {
                    StartAngle = -150,
                    EndAngle = 150,
                    Background = new[]
                  {
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#FFF" }, { 1, "#333" } }
                            }),
                            BorderWidth = new PercentageOrPixel(2),
                            OuterRadius = new PercentageOrPixel(109, true)
                        },
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#333" }, { 1, "#FFF" } }
                            }),
                            BorderWidth = new PercentageOrPixel(1),
                            OuterRadius = new PercentageOrPixel(107, true)
                        },
                        new BackgroundObject(),
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(Color.LightGray),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(105, true),
                            InnerRadius = new PercentageOrPixel(102, true)
                        }
                  }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,

                    MinorTickWidth = 1,
                    MinorTickLength = 15,
                    MinorTickPosition = TickPositions.Inside,
                    MinorTickColor = Color.Black,
                    TickInterval = 5,
                    TickWidth = 1,
                    TickPosition = TickPositions.Inside,
                    TickLength = 30,
                    TickColor = Color.Black,
                    Labels = new YAxisLabels
                    {
                        Step = 2,
                        Distance = -43,
                        Style = "color:'black', fontSize:'15px', fontWeight:'bold'",

                    },
                    Title = new YAxisTitle { Text = "%", Style = "color:'black', fontWeight:'bold', fontSize:'23px'" },
                    PlotBands = new[]
                  {
                        new YAxisPlotBands { From = 0, To = 40, Color = Color.Tomato,  InnerRadius = new PercentageOrPixel(35, true) },
                        new YAxisPlotBands { From = 40, To = 80, Color = Color.Yellow, InnerRadius = new PercentageOrPixel(35, true) },
                        new YAxisPlotBands { From = 80, To = 100, Color = Color.LightGreen, InnerRadius = new PercentageOrPixel(35, true) }
                  }
                })
                .SetPlotOptions(new PlotOptions
                {
                    Gauge = new PlotOptionsGauge
                    {
                        DataLabels = new PlotOptionsGaugeDataLabels
                        {
                            Style = "color:'black', fontSize:'26px', fontWeight:'bold'",
                            VerticalAlign = VerticalAligns.Bottom,
                            Y = 140,

                        }
                    }
                })
                .SetSeries(new Series
                {
                    Name = "Result",
                    Data = new Data(new object[] { assemblyPercentage }),
                });

                assembly1.Text = chart1.ToHtmlString();

            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void btnMachining_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Backups/MachiningStatus.aspx");
        }

        protected void btnAssembly_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Backups/AssemblyStatus.aspx");
        }
    }
}