const master_kpi_widgets = {
    listed_doctor: {
        label: "Listed Doctors",
        views: {
            speciality: {
                label: "Speciality",
                widgetName: "Speciality wise Listed Doctors",
                allowed_charts: ['pie','donut','column', 'line', 'area','funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                    campaign: {
                        label: "Campaign"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise Listed Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    class: {
                        label: "Class"
                    },
                    campaign: {
                        label: "Campaign"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            class: {
                label: "Class",
                widgetName: "Class wise Listed Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    category: {
                        label: "Category"
                    },
                    campaign: {
                        label: "Campaign"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            campaign: {
                label: "Campaign",
                widgetName: "Campaign wise Listed Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
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
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            subdivision: {
                label: "Subdivision",
                widgetName: "Subdivision wise Listed Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
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
                    campaign: {
                        label: "Campaign"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            state: {
                label: "State wise",
                widgetName: "State wise Listed Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
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
                    campaign: {
                        label: "Campaign"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                }
            },

        }
    },
    un_listed_doctor: {
        label: "Unlisted Doctors",
        views: {
            speciality: {
                label: "Speciality",
                widgetName: "Speciality wise Unlisted Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise Unlisted Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    class: {
                        label: "Class"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            class: {
                label: "Class",
                widgetName: "Class wise Unlisted Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    speciality: {
                        label: "Speciality"
                    },
                    category: {
                        label: "Category"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            subdivision: {
                label: "Subdivision",
                widgetName: "Subdivision wise Unlisted Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
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
                    state: {
                        label: "State wise"
                    },
                }
            },
            state: {
                label: "State wise",
                widgetName: "State wise Unlisted Doctors",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
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
                    subdivision: {
                        label: "Subdivision"
                    },
                }
            },
        }
    },
    chemist: {
        label: "Chemist",
        views: {
            category: {
                label: "Category",
                widgetName: "Category wise Chemists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    class: {
                        label: "Class"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }

            },
            class: {
                label: "Class",
                widgetName: "Class wise Chemists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            subdivision: {
                label: "Subdivision",
                widgetName: "Subdivision wise Chemists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                    state: {
                        label: "State wise"
                    },
                }
            },
            state: {
                label: "State wise",
                widgetName: "State wise Chemists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    class: {
                        label: "Class"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                }
            },
        }
    },
    stockist: {
        label: "Stockist",
        views: {
            hq: {
                label: "HQ Wise",
                widgetName: "HQ wise Stockists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    subdivision: {
                        label: "Subdivision"
                    },
                    state: {
                        label: "State Wise",
                    }
                }
            },
            subdivision: {
                label: "Subdivision",
                widgetName: "Subdivision wise Stockists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    hq: {
                        label: "HQ Wise"
                    },
                    state: {
                        label: "State Wise",
                    }
                }
            },
            state: {
                label: "State Wise",
                widgetName: "State wise Stockists",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    hq: {
                        label: "HQ Wise"
                    },
                    subdivision: {
                        label: "Subdivision"
                    },
                }
            },
        }
    },
    fieldforce: {
        label: "Field Force",
        views: {
            subdivision: {
                label: "Sub Division",
                widgetName: "Sub Division wise Field Forces",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    state: {
                        label: "State Wise"
                    },
                }
            },
            state: {
                label: "State Wise",
                widgetName: "State wise Field Forces",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    subdivision: {
                        label: "Sub Division"
                    },
                }
            },
        }
    },
    product: {
        label: "Product",
        views: {
            category: {
                label: "Category",
                widgetName: "Category wise Products",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    brand: {
                        label: "Brand"
                    },
                    group: {
                        label: "Group"
                    },
                    subdivision: {
                        label: "Sub Division"
                    },
                    state: {
                        label: "State Wise"
                    },
                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise Products",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    group: {
                        label: "Group"
                    },
                    subdivision: {
                        label: "Sub Division"
                    },
                    state: {
                        label: "State Wise"
                    },
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise Products",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    brand: {
                        label: "Brand"
                    },
                    subdivision: {
                        label: "Sub Division"
                    },
                    state: {
                        label: "State Wise"
                    },
                }
            },
            subdivision: {
                label: "Sub Division",
                widgetName: "Sub Division wise Products",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    brand: {
                        label: "Brand"
                    },
                    group: {
                        label: "Group"
                    },
                    state: {
                        label: "State Wise"
                    },
                }
            },
            state: {
                label: "State Wise",
                widgetName: "State wise Products",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
                split_by: {
                    category: {
                        label: "Category"
                    },
                    brand: {
                        label: "Brand"
                    },
                    group: {
                        label: "Group"
                    },
                    subdivision: {
                        label: "Sub Division"
                    },
                }
            },
        }
    },
    holiday: {
        label: "Holiday",
        views: {
            state: {
                label: "State Wise",
                widgetName: "State wise Holidays",
                allowed_charts: ['pie', 'donut', 'column', 'line', 'area', 'funnel', 'table'],
            },
        }
    },
}
