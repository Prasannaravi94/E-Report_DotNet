
const marketing_kpi_widgets = {
    brand_visited: {
        label: "Brand Visited",
        views: {
            priority: {
                label: "Priority",
                widgetName: "Priority Brand Wise visited",
                allowed_charts: ['linecolumn','table'],
                default_chart: 'linecolumn',
                filters: {
                    brands: {
                        label: "Brands",
                        type: "select",
                        multiple:true,
                        alloption: true,
                        alltext: "All Brands",
                        options: getBrandOptions
                    }
                }
            },
            potential_yield: {
                widgetName: "Brand wise Potential & Yield",
                label: "Potential & Yield",
                allowed_charts: ['column','line','area','table'],
                default_chart: 'column',
                //filters: {
                //    brands: {
                //        label: "Docotors",
                //        type: "select",
                //        multiple: true,
                //        alloption: true,
                //        options: getDoctorOptions
                //    }
                //}
            },

        }
    },
    product_visited: {
        label: "Product Visited",
        views: {
            priority: {
                label: "Priority",
                widgetName: "Priority Product Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        options: getProductOptions
                    }
                }
            },
            potential_yield: {
                widgetName: "Product wise Potential & Yield",
                label: "Potential & Yield",
                allowed_charts: ['column', 'line', 'area', 'table'],
                default_chart: 'column',
                filters: {
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        max: 10,
                        min: 5,
                        alltext:"First 5 Products",
                        options: getProductOptions
                    }
                }
            },

        }
    },
    group_visited: {
        label: "Group Visited",
        views: {
            priority: {
                label: "Priority",
                widgetName: "Priority Group Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    groups: {
                        label: "Groups",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Groups",
                        options: getGroupsOptions
                    }
                }
            },
            potential_yield: {
                widgetName: "Group wise Potential & Yield",
                label: "Potential & Yield",
                allowed_charts: ['column', 'line', 'area', 'table'],
                default_chart: 'column',
                filters: {
                    groups: {
                        label: "Group",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Groups",
                        options: getGroupsOptions
                    }
                }
            },

        }
    },
    exposure: {
        label: "Exposure",
        views: {
            product: {
                label: "Product",
                widgetName: "Product Wise Exposure",
                allowed_charts: ['column', 'line', 'area', 'table'],
                default_chart: 'column',
                filters: {
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        max: 10,
                        min: 5,
                        alltext: "First 5 Products",
                        options: getProductOptions
                    }
                },
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                }
            },
            campaign: {
                widgetName: "Campaign wise Exposure",
                label: "Campaign",
                allowed_charts: ['column', 'line', 'area', 'table'],
                default_chart: 'column',
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                },
                filters: {
                    subcategories: {
                        label: "Campaigns",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        max: 10,
                        min: 5,
                        alltext: "All Campaigns",
                        options: getSubCategoryOptions
                    }
                },
            },

        }
    },
    campaign: {
        label: "Campaigns",
        views: {
            brand: {
                label: "Brand",
                widgetName: "Campaign Brand Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    brands: {
                        label: "Brands",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Brands",
                        max: 10,
                        min: 5,
                        options: getBrandOptions
                    }
                }
            },
            product: {
                label: "Products",
                widgetName: "Campaign Product Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        max: 10,
                        min: 5,
                        options: getProductOptions
                    }
                }
            },
            speciality: {
                label: "Specialities",
                widgetName: "Campaign Speciality Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    specialities: {
                        label: "Specialities",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Specialities",
                        max: 10,
                        min: 5,
                        options: getSpecialityOptions
                    }
                }
            },
            categroy: {
                label: "Category",
                widgetName: "Campaign Category Wise visited",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    categories: {
                        label: "Categories",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Categories",
                        max: 10,
                        min: 5,
                        options: getCategoryOptions
                    }
                }
            },
        }
    },
    doctor_business: {
        label: "Doctor Business",
        views: {
            campaign: {
                label: "Campaign",
                widgetName: "Campaign Wise Doctor Business",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            speciality: {
                label: "Speciality",
                widgetName: "Speciality Wise Doctor Business",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            category: {
                label: "Category",
                widgetName: "Category Wise Sample Issued",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            class: {
                label: "Class",
                widgetName: "Class Wise Sample Issued",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
        }
    },
    sample_issued: {
        label: "Sample / Input Issued",
        views: {
            hq: {
                label: "HQ wise",
                widgetName: "HQ Wise Sample Issued",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            campaign: {
                label: "Campaign",
                widgetName: "Campaign Wise Sample Issued",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
        }
    },
    digital_detailing: {
        label: "Digital Detailing",
        views: {
            brand: {
                label: "Brand",
                widgetName: "Brand wise Digital Detailing",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            speciality: {
                label: "Speciality",
                widgetName: "Speciality wise Digital Detailing",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
            product: {
                label: "Product",
                widgetName: "Product wise Digital Detailing",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true, 
                        alloption: true,
                        alltext: "First 5 Products",
                        max: 10,
                        min: 5,
                        options: getProductOptions
                    }
                }
            },
            user: {
                label: "User",
                widgetName: "User wise Digital Detailing",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
                filters: {
                    users: {
                        label: "User",
                        type: "select",
                        multiple: true,
                        required: true,
                        alloption: true,
                        alltext: "Select User",
                        max: 10,
                        min: 5,
                        options: getFieldForceOptions
                    }
                }
            },
            priority: {
                label: "Priority",
                widgetName: "Priority wise Digital Detailing",
                allowed_charts: ['linecolumn', 'table'],
                default_chart: 'linecolumn',
            },
        }
    },

    
}
    function MarketingKpiLineColumnChartOptions(data, params) {

        if (typeof params !="undefined" && params.measureby == 'doctor_business') {
            data.series[0]['type'] = 'column';
            data.series[1]['type'] = 'column';
            data.series[2]['type'] = 'column';
            data.series[3]['type'] = 'line';
            var options = {
                series: data.series,
                chart: {
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false,
                    },
                    events: events,
                    type: 'line',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: true,
                    formatter: function (val, opts) {
                        if (opts.seriesIndex == 3)
                            return val + "%"
                        else
                            return val
                    },
                },
                stroke: {
                    show: true,
                    width: [1, 1,1, 4],
                },
                xaxis: {
                    categories: data.labels,
                
                    title: {
                        text: data.view_by_title,
                    
                    },
                },
                yaxis: [
                    {
                        seriesName: data.series[0].name,
                        labels: {
                            style: {
                                colors: Apex.colors[0],
                            }
                        },
                        title: {
                            text: data.measure_by_title,
                            style: {
                                color: Apex.colors[0],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        show: false,
                        seriesName: data.series[0].name,
                    },
                    {
                        seriesName: data.series[2].name,
                        labels: {
                            style: {
                                colors: Apex.colors[2],
                            }
                        },
                        title: {
                            text: data.series[2].name,
                            style: {
                                color: Apex.colors[2],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        min: 0,
                        tickAmount: 4,
                        max: 100,
                        seriesName: data.series[3].name,
                        opposite: true,
                        labels: {
                            style: {
                                colors: Apex.colors[3],
                            }
                        },
                        title: {
                            text: data.series[3].name,
                            style: {
                                color: Apex.colors[3],
                            }
                        },
                    },
                ],
                tooltip: {
                    enabled: true,
                    intersect: false,
                },
            };
        } else if (typeof params != "undefined" && params.measureby == 'sample_issued') {
            data.series[0]['type'] = 'column';
            data.series[1]['type'] = 'line';
            data.series[2]['type'] = 'column';
            data.series[3]['type'] = 'line';
            var options = {
                series: data.series,
                chart: {
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false,
                    },
                    events: events,
                    type: 'line',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: true,
                },
                stroke: {
                    show: true,
                    width: [1, 4,1, 4],
                },
                xaxis: {
                    categories: data.labels,

                    title: {
                        text: data.view_by_title,

                    },
                },
                yaxis: [
                    {
                        seriesName: data.series[0].name,
                        labels: {
                            style: {
                                colors: Apex.colors[0],
                            }
                        },
                        title: {
                            text: 'Quantity',
                            style: {
                                color: Apex.colors[0],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        opposite:true,
                        seriesName: data.series[1].name,
                        labels: {
                            style: {
                                colors: Apex.colors[1],
                            }
                        },
                        title: {
                            text: 'Value',
                            style: {
                                color: Apex.colors[1],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        show: false,
                        seriesName: data.series[0].name,
                    },
                    {
                        show: false,
                        seriesName: data.series[1].name,
                    },
                ],
                tooltip: {
                    enabled: true,
                    intersect: false,
                },
            };
        }
        else if (typeof params != "undefined" && params.measureby == 'digital_detailing') {
            data.series[0]['type'] = 'column';
            data.series[1]['type'] = 'line';
            var options = {
                series: data.series,
                chart: {
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false,
                    },
                    events: events,
                    type: 'line',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: true,
                    formatter: function (val, opts) {
                        if (opts.seriesIndex == 2)
                            return val + "%"
                        else
                            return val
                    },
                },
                stroke: {
                    show: true,
                    width: [1, 4],
                },
                xaxis: {
                    categories: data.labels,
                    title: {
                        text: data.view_by_title
                    },
                },
                yaxis: [
                    {
                        seriesName: data.series[0].name,
                        labels: {
                            style: {
                                colors: Apex.colors[0],
                            }
                        },
                        title: {
                            text: data.series[0].name,
                            style: {
                                color: Apex.colors[0],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        opposite: true,
                        seriesName: data.series[1].name,
                        labels: {
                            style: {
                                colors: Apex.colors[1],
                            }
                        },
                        title: {
                            text: data.series[1].name,
                            style: {
                                color: Apex.colors[1],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },

                ],
                tooltip: {
                    enabled: true,
                    intersect: false,
                },
            };
        }
        else {
            data.series[0]['type'] = 'column';
            data.series[1]['type'] = 'column';
            data.series[2]['type'] = 'line';
            var options = {
                series: data.series,
                chart: {
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false,
                    },
                    events: events,
                    type: 'line',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: true,
                    formatter: function (val, opts) {
                        if (opts.seriesIndex == 2)
                            return val + "%"
                        else
                            return val
                    },
                },
                stroke: {
                    show: true,
                    width: [1, 1, 4],
                },
                xaxis: {
                    categories: data.labels,
                    title: {
                        text: data.view_by_title
                    },
                },
                yaxis: [
                    {
                        seriesName: data.series[0].name,
                        labels: {
                            style: {
                                colors: Apex.colors[0],
                            }
                        },
                        title: {
                            text: data.measure_by_title,
                            style: {
                                color: Apex.colors[0],
                            }
                        },
                        tooltip: {
                            enabled: false
                        }
                    },
                    {
                        show: false,
                        seriesName: data.series[0].name,
                    },
                    {
                        min: 0,
                        tickAmount: 4,
                        max: 100,
                        seriesName: data.series[2].name,
                        opposite: true,
                        labels: {
                            style: {
                                colors: Apex.colors[2],
                            }
                        },
                        title: {
                            text: data.series[2].name,
                            style: {
                                color: Apex.colors[2],
                            }
                        },
                    },
                ],
                tooltip: {
                    enabled: true,
                    intersect: false,
                },
            };
        }


        return options;
    }