using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Drawing;
using Point = DotNet.Highcharts.Options.Point;

namespace PlantControl.Views
{
    public partial class LimitsReviewOverall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsReviewOverall");
            if (authorizated)
            {
                SQLQuery sQLQuery = new SQLQuery();

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
                   Height = 290,
                   Width = 290

               })
               .SetTitle(new Title { 
                   Text = "Plant Review %",
                   Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
               })
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
                   TickLength = 20,
                   TickInterval = 5,
                   TickColor = Color.Black,
                   Labels = new YAxisLabels
                   {
                       Step = 2,
                       Distance = -33,
                       Style = "color:'black', fontSize:'12px', fontWeight:'bold'",

                   },
                   Title = new YAxisTitle { 
                       Text = "%", 
                       Style = "color:'black', fontWeight:'bold', fontSize:'19px'" ,
                       Align = AxisTitleAligns.Middle
                   },
                   PlotBands = new[]
                   {
                        new YAxisPlotBands { From = 0, To = 100, Color = Color.LightGreen, InnerRadius = new PercentageOrPixel(35, true) }
                   }
               })
               .SetPlotOptions(new PlotOptions
               {
                   Gauge = new PlotOptionsGauge
                   {
                       DataLabels = new PlotOptionsGaugeDataLabels
                       {
                           Style = "color:'black', fontSize:'17px', fontWeight:'bold'",
                           Y = 100
                       }
                   }
               })
               .SetSeries(new Series
               {
                   Name = "Result",
                   Data = new Data(new object[] { sQLQuery.GetLimitsReviewTotal() }),
               });
                AllChart.Text = chart.ToHtmlString();
              

                Highcharts chart1 = new Highcharts("chart1")
                .InitChart(new Chart { 
                    Type = ChartTypes.Bar ,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title { 
                    Text = "MAN Prop Shaft",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle { 
                    Text= "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis{ 
                    
                })
                .SetYAxis(new YAxis{
                              Min = 0,
                              StackLabels = new YAxisStackLabels{
                                          Enabled = true,
                                          Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                              }
                              })
                .SetLegend(new Legend{
                              X = 10,
                              Y = 10,
                              Floating = true,
                              BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                              Shadow = true
                                            })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions{                          
                              Bar = new PlotOptionsBar
                              {
                                  PointPadding = 3,
                                  BorderWidth = 0.5,
                                  PointWidth = 20, 
                                  DataLabels = new PlotOptionsBarDataLabels
                                  {
                                      Enabled = true,
                                      Color = Color.Black,
                                      Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                                  }
                              },

                              Series = new PlotOptionsSeries{
                                            Point = new PlotOptionsSeriesPoint{
                                                        Events = new PlotOptionsSeriesPointEvents
                                                        {
                                                            //Click = @"function() {
                                                            //location.href = this.id;}"
                                                        }
                                            },
                                    },
                              })
                .SetSeries(new[]{                           
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_MAN_CARDAN") }), Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_MAN_CARDAN") }), Color = Color.LightGreen},
                                            });
                OneChart1.Text = chart1.ToHtmlString();


                Highcharts chart2 = new Highcharts("chart2")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "MAN 3rdM",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_MAN_3RDM") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_MAN_3RDM") }), Color = Color.LightGreen},
                                            });
                OneChart2.Text = chart2.ToHtmlString();


                Highcharts chart3 = new Highcharts("chart3")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "MAN Final",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_MAN_FINAL") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_MAN_FINAL") }), Color = Color.LightGreen},
                                            });
                OneChart3.Text = chart3.ToHtmlString();


                Highcharts chart4 = new Highcharts("chart4")
                .InitChart(new Chart{
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "MAN HUB",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_MAN_HUB") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_MAN_HUB") }), Color = Color.LightGreen},
                                            });
                OneChart4.Text = chart4.ToHtmlString();


                Highcharts chart5 = new Highcharts("chart5")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "Front Axle",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_FRONT") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_FRONT") }), Color = Color.LightGreen},
                                            });
                OneChart5.Text = chart5.ToHtmlString();


                Highcharts chart6 = new Highcharts("chart6")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "RPU 3rdM",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_RPU_3RDM") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_RPU_3RDM") }), Color = Color.LightGreen},
                                            });
                OneChart6.Text = chart6.ToHtmlString();


                Highcharts chart7 = new Highcharts("chart7")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "RPU Final",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_RPU_FINAL") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_RPU_FINAL") }), Color = Color.LightGreen},
                                            });
                OneChart7.Text = chart7.ToHtmlString();


                Highcharts chart8 = new Highcharts("chart8")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "RPU Axle Shaft",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_RPU_SHAFT") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_RPU_SHAFT") }), Color = Color.LightGreen},
                                            });
                OneChart8.Text = chart8.ToHtmlString();


                Highcharts chart9 = new Highcharts("chart9")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "Banjo Weld",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_BANJO")}),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_BANJO")}), Color = Color.LightGreen},
                                            });
                OneChart9.Text = chart9.ToHtmlString();


                Highcharts chart10 = new Highcharts("chart10")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    Height = 160,
                    Width = 235,
                    MarginTop = 30
                })
                .SetTitle(new Title{
                    Text = "CV Joints",
                    Style = "fontWeight:'bold', color: 'black', fontSize: '15px'"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Limits",
                    Style = "fontWeight:'regular', color: 'black', fontSize: '13px'",
                    X = 10,
                    Y = 30
                })
                .SetXAxis(new XAxis
                {

                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    StackLabels = new YAxisStackLabels
                    {
                        Enabled = true,
                        Style = "fontWeight:'bold', color: 'black', fontSize: '18px'"
                    }
                })
                .SetLegend(new Legend
                {
                    X = 10,
                    Y = 10,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.y ; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        PointPadding = 3,
                        BorderWidth = 0.5,
                        PointWidth = 20,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Color = Color.Black,
                            Style = "fontSize:'13px', fontFamily:'Verdana', fontWeight:'bold'"
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                //Click = @"function() {
                                //location.href = this.id;}"
                            }
                        },
                    },
                })
                .SetSeries(new[]{
                                new Series { Name = "Total", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfLimits("LIMITS_CVJ") }),  Color = Color.CornflowerBlue},
                                new Series { Name = "Reviewed", Data = new Data(new object[] {sQLQuery.GetLimitsReviewNumOfReviewed("LIMITS_CVJ") }), Color = Color.LightGreen},
                                            });
                OneChart10.Text = chart10.ToHtmlString();

            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }                
        }
    }
}