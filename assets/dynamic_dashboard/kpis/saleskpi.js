const sales_kpi_priodtype_filter = {
    label: "Period Type",
    type: "select",
    multiple: false,
    alloption: false,
    required: true,
    alltext: "Select Type",
    options: getPeriodTypes
};
const sales_kpi_priod_filter = {
    label: "Period",
    type: "select",
    multiple: false,
    alloption: false,
    required: true,
    alltext: "Select Period",
    options: getPeriods
};
const sales_kpi_comparison_priod_filter = {
    label: "Comparison Period",
    type: "select",
    multiple: false,
    alloption: false,
    required: true,
    alltext: "Select Period",
    options: getComparisonPeriods
};
const sales_kpi_measure_by_filter = {
    label: "Measure By",
    type: "select",
    multiple: false,
    alloption: false,
    required: true,
    alltext: "Select Type",
    options: getMeasurebyTypes
};

const sales_kpi_brands_filter = {
    label: "Brands",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All Brands",
    required: false,
    options: getBrandOptions
}
const sales_kpi_category_filter = {
    label: "Categories",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All Categories",
    required: false,
    options: getProductCategoryOptions
}
const sales_kpi_group_filter = {
    label: "Groups",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All Groups",
    required: false,
    options: getGroupsOptions
}
const sales_kpi_state_filter = {
    label: "States",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All States",
    required: false,
    options: getStateOptions
}
const sales_kpi_hq_filter = {
    label: "HQs",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All HQs",
    required: false,
    options: getHQOptions
}
const sales_kpi_product_filter = {
    label: "Products",
    type: "select",
    multiple: true,
    alloption: true,
    alltext: "All Products",
    required: false,
    max: 10,
    options: getProductOptions
}
const sales_kpi_widgets = {
    primary_sales: {
        label: "Primary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise Primary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: sales_kpi_product_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise Primary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise Primary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise Primary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise Primary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
    secondary_sales: {
        label: "Secondary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise Secondary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        required: false,
                        max: 10,
                        options: getProductOptions
                    },
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise Secondary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise Secondary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise Secondary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise Secondary Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
    primary_secondary_sales: {
        label: "Primary/Secondary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        required: false,
                        max: 10,
                        options: getProductOptions
                    },
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
    target_primary_sales: {
        label: "Target vs Primary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        required: false,
                        max: 10,
                        options: getProductOptions
                    },
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
    target_secondary_sales: {
        label: "Target vs Secondary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise T vs S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        required: false,
                        max: 10,
                        options: getProductOptions
                    },
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise T vs S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise T vs S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise T vs S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise T vs S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
    target_primary_secondary_sales: {
        label: "Target vs Primary/Secondary Sales",
        views: {
            product: {
                label: "Product",
                widgetName: "Product wise T vs P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    products: {
                        label: "Products",
                        type: "select",
                        multiple: true,
                        alloption: true,
                        alltext: "All Products",
                        required: false,
                        max: 10,
                        options: getProductOptions
                    },
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,

                }
            },
            brand: {
                label: "Brand",
                widgetName: "Brand wise T vs P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    categories: sales_kpi_category_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            category: {
                label: "Category",
                widgetName: "Category wise T vs P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            group: {
                label: "Group",
                widgetName: "Group wise T vs P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    brands: sales_kpi_brands_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            state: {
                label: "State",
                widgetName: "State wise T vs P/S Sales",
                allowed_charts: ['linecolumn', 'table','score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    hqs: sales_kpi_hq_filter,
                }
            },
            hq: {
                label: "HQ",
                widgetName: "HQ wise T vs P Sales",
                allowed_charts: ['linecolumn', 'table', 'score'],
                default_chart: 'linecolumn',
                filters: {
                    measure_by: sales_kpi_measure_by_filter,
                    periodtype: sales_kpi_priodtype_filter,
                    period: sales_kpi_priod_filter,
                    comparison_period: sales_kpi_comparison_priod_filter,
                    products: sales_kpi_product_filter,
                    brands: sales_kpi_brands_filter,
                    groups: sales_kpi_group_filter,
                    categories: sales_kpi_category_filter,
                    states: sales_kpi_state_filter,
                }
            },
        }
    },
}
function getPeriodTypes(filterwrapper, callback) {
    callback(
        `
        <option value="MTD">MTD</option>
        <option value="QTD">QTD</option>
        <option value="YTD">YTD</option>
        `
    )
}
function setPeriods(options, periodinput) {
    periodinput.selectpicker('destroy');
    periodinput.empty();
    periodinput.append(options);
    periodinput.selectpicker();
}
function subtractMonthsFromDate(date, months) {
    const newDate = new Date(date);
    newDate.setMonth(newDate.getMonth() - months);
    return newDate;
}
function getEndDate(periodtype, period) {
    minusMonth = 0;
    if (periodtype === "MTD") {
        minusMonth = 1;
        const monthStart = period.split('-');
        EndMonth = parseInt(monthStart[0]);
        EndYear = parseInt(monthStart[1]);
    } else if (periodtype === "QTD") {
        minusMonth = 3;
        const monthStart = period.split('-');
        switch (parseInt(monthStart[0])) {
            case 1:
                EndMonth = 3;
                break;
            case 2:
                EndMonth = 6;
                break;
            case 3:
                EndMonth = 9;
                break;
            case 4:
                EndMonth = 12;
                break;
            default:
                break;
        }
        EndYear = parseInt(monthStart[1]);
    } else if (periodtype === "YTD") {
        minusMonth = 12;
        const monthStart = period.split('-');
        EndMonth = 12;
        EndYear = parseInt(monthStart[0]);
    }

    endDate = EndYear + "-" + EndMonth + "-" + new Date(EndYear, EndMonth, 0).getDate();

    return subtractMonthsFromDate(endDate, minusMonth);
}


$('body').on('change', '.widget-form-filter[name="periodtype"]', function () {
    var options = getPeriodsOptions($(this).val(), new Date());
    var periodinput = $(this).closest('.widget-custom-filter-wrapper').find('.widget-form-filter[name="period"]');
    setPeriods(options, periodinput);
    periodinput.trigger('change');
});
$('body').on('change', '.widget-form-filter[name="period"]', function () {
    var periodtype = $(this).closest('.widget-custom-filter-wrapper').find('.widget-form-filter[name="periodtype"]');
    var options = getPeriodsOptions(periodtype.val(), new Date(getEndDate(periodtype.val(), $(this).val())));
    var comparison_period = $(this).closest('.widget-custom-filter-wrapper').find('.widget-form-filter[name="comparison_period"]');
    setPeriods(options, comparison_period);
});
function getPeriodsOptions(periodType, currentDate) {
    const currentYear = currentDate.getFullYear();
    const currentMonth = currentDate.getMonth() + 1; // Months are zero-indexed

    let periodsHTML = '';
    switch (periodType) {
        case 'MTD':
            for (let year = currentYear; year >= currentYear - 2; year--) {
                const endMonth = (year === currentYear) ? currentMonth : 12;

                for (let month = endMonth; month >= 1; month--) {
                    const formattedMonth = month.toString().padStart(2, '0');
                    const period = `${formattedMonth}-${year}`;

                    // Check if the period is not greater than the current date
                    if (year < currentYear || (year === currentYear && month <= currentMonth)) {
                        periodsHTML += `<option value="${period}">${getMonthName(month)} ${year}</option>`;
                    }
                }
            }
            break;

        case 'YTD':
            for (let year = currentYear; year >= currentYear - 2; year--) {
                // Check if the year is not greater than the current year
                if (year <= currentYear) {
                    periodsHTML += `<option value="${year}">${year}</option>`;
                }
            }
            break;

        case 'QTD':
            for (let year = currentYear; year >= currentYear - 2; year--) {
                const endQuarter = (year === currentYear) ? Math.ceil(currentMonth / 3) : 4;

                for (let quarter = endQuarter; quarter >= 1; quarter--) {
                    const period = `${quarter}-${year}`;

                    // Check if the period is not greater than the current date
                    if (year < currentYear || (year === currentYear)) {
                        periodsHTML += `<option value="${period}">${getQuarterName(quarter)} ${year}</option>`;
                    }
                }
            }
            break;

        default:
            break;
    }
    return periodsHTML;
}
function getPeriods(filterwrapper, callback) {

    var periodsHTML = getPeriodsOptions($(filterwrapper + ' .widget-form-filter[name="periodtype"]').val(), new Date())
    callback(periodsHTML);
}
function getComparisonPeriods(filterwrapper, callback) {
    var periodtype = $(filterwrapper + ' .widget-form-filter[name="periodtype"]');
    var periodinput = $(filterwrapper + ' .widget-form-filter[name="period"]');
    var periodsHTML = getPeriodsOptions(periodtype.val(), new Date(getEndDate(periodtype.val(), periodinput.val())));
    callback(periodsHTML);
}

function getMonthName(month) {
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    return monthNames[month - 1];
}

function getQuarterName(quarter) {
    return `Q${quarter}`;
}

function getMeasurebyTypes(filterwrapper, callback) {
    var options = `
    <option value='value'>Sale Value</option>
    <option value='quantity'> Sale Quantity</option>
    `;
    callback(options);
}

function SalesKpiLineColumnChartOptions(data, params) {

    if (typeof params != "undefined" && (params.measureby == 'primary_sales' || params.measureby == 'secondary_sales')) {
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
                    if (opts.seriesIndex == 1)
                        return val + "%"
                    else
                        return val
                },
            },
            stroke: {
                show: true,
                width: [1, 0],
            },
            xaxis: {
                categories: data.labels,
                title: {
                    text: data.view_by_title
                },
            },
            yaxis: [
                {
                    min: 0,
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
                    seriesName: data.series[1].name,
                    opposite: true,
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
                },
            ],
            tooltip: {
                enabled: true,
                intersect: false,
            },
        };
    }
    else if (typeof params != "undefined" && params.measureby == 'target_primary_secondary_sales') {
        data.series[0]['type'] = 'column';
        data.series[1]['type'] = 'column';
        data.series[2]['type'] = 'column';
        data.series[3]['type'] = 'line';
        data.series[4]['type'] = 'line';
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
                    if (opts.seriesIndex == 3 || opts.seriesIndex == 4)
                        return val + "%"
                    else
                        return val
                },
            },
            stroke: {
                show: true,
                width: [1, 1, 1, 0, 0],
            },
            xaxis: {
                categories: data.labels,
                title: {
                    text: data.view_by_title
                },
            },
            yaxis: [
                {
                    min: 0,
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
                    show: false,
                    seriesName: data.series[0].name,
                },
                {
                    seriesName: data.series[3].name,
                    opposite: true,
                    labels: {
                        style: {
                            colors: Apex.colors[3],
                        }
                    },
                    title: {
                        text: 'Growth',
                        style: {
                            color: Apex.colors[3],
                        }
                    },
                },
                {
                    seriesName: data.series[4].name,
                    show: false,
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
                width: [1, 1, 0],
            },
            xaxis: {
                categories: data.labels,
                title: {
                    text: data.view_by_title
                },
            },
            yaxis: [
                {
                    min: 0,
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
                    opposite: true,
                    labels: {
                        style: {
                            colors: Apex.colors[2],
                        }
                    },
                    title: {
                        text: 'Growth',
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

function SetScoreCard(data, params) {
    var string = `
        <div class="row text-center justify-content-around">`;
    if (typeof params != "undefined") {
        if (params.measureby == "primary_sales") {
            var primarySale = calculateArraySum(data.series[0].data);
            var primaryGrowth = calculateArrayAverage(data.series[1].data);
        } else if (params.measureby == "secondary_sales") {
            var secondarySale = calculateArraySum(data.series[0].data);
            var secondaryGrowth = calculateArrayAverage(data.series[1].data);
        } else if (params.measureby == "primary_secondary_sales") {
            var primarySale = calculateArraySum(data.series[0].data);
            var secondarySale = calculateArrayAverage(data.series[1].data);
            var primaryGrowth = calculateArrayAverage(data.series[2].data);
            var secondaryGrowth = calculateArrayAverage(data.series[3].data);
        }
        else if (params.measureby == "target_primary_sales") {
            var target = calculateArraySum(data.series[0].data);
            var primarySale = calculateArraySum(data.series[1].data);
            var primaryGrowth = calculateArrayAverage(data.series[2].data);
        }
        else if (params.measureby == "target_secondary_sales") {
            var target = calculateArraySum(data.series[0].data);
            var secondarySale = calculateArraySum(data.series[1].data);
            var secondaryGrowth = calculateArrayAverage(data.series[2].data);
        }
        else if (params.measureby == "target_primary_secondary_sales") {
            var target = calculateArraySum(data.series[0].data);
            var primarySale = calculateArraySum(data.series[1].data);
            var secondarySale = calculateArraySum(data.series[2].data);
            var primaryGrowth = calculateArrayAverage(data.series[2].data);
            var secondaryGrowth = calculateArrayAverage(data.series[2].data);
        }
        
        if (params.measureby == "target_primary_sales" || params.measureby == "target_secondary_sales" || params.measureby == "target_primary_secondary_sales") {
            string += `
            <div class="w-auto pt-3 text-center">
                <div>
                <h4 class="mb-1" data-bs-toggle="tooltip" data-bs-placement="top" title="${target}">${formatNumberAbbreviation(target)}</h4>
                <p class="mb-0 text-muted">Target</p>
                </div>
            </div>
        `

        }
        if (params.measureby == "primary_sales" || params.measureby == "primary_secondary_sales" || params.measureby == "target_primary_sales" || params.measureby == "target_primary_secondary_sales") {
            growthclass = 'success';
            growthicon = '<i class="fa-solid fa-arrow-trend-up"></i>';
            if (primaryGrowth < 0) {
                growthclass = 'danger';
                growthicon = '<i class="fa-solid fa-arrow-trend-down"></i>'
            }
            string += `
            <div class="w-auto pt-3 text-center">
                <div>
                <h4 class="mb-1" data-bs-toggle="tooltip" data-bs-placement="top" title="${primarySale}">${formatNumberAbbreviation(primarySale)}</h4>
                <p class="mb-0 text-muted">Primary Sales</p>
                <p class="mb-0 text-${growthclass}" data-bs-toggle="tooltip" data-bs-placement="top" title="Growth">${growthicon} ${primaryGrowth}%</p>
                </div>
            </div>
        `

        }
        if (params.measureby == "secondary_sales" || params.measureby == "primary_secondary_sales" || params.measureby == "target_secondary_sales" || params.measureby == "target_primary_secondary_sales") {
            growthclass = 'success';
            growthicon = '<i class="fa-solid fa-arrow-trend-up"></i>';
            if (secondaryGrowth < 0) {
                growthclass = 'danger';
                growthicon = '<i class="fa-solid fa-arrow-trend-down"></i>'
            }
            string += `
            <div class="w-auto pt-3 text-center">
                <div>
                <h4 class="mb-1" data-bs-toggle="tooltip" data-bs-placement="top" title="${secondarySale}">${formatNumberAbbreviation(secondarySale)}</h4>
                <p class="mb-0 text-muted">Secondary Sales</p>
                <p class="mb-0 text-${growthclass}" data-bs-toggle="tooltip" data-bs-placement="top" title="Growth">${growthicon} ${secondaryGrowth}%</p>
                </div>
            </div>
        `

        }
    }
    else {
        string += `
            <div class="w-auto pt-3 text-center">
                <div>
                <h4 class="mb-1" data-bs-toggle="tooltip" data-bs-placement="top" title="111166">111.17k</h4>
                <p class="mb-0 text-muted">Primary Sales</p>
                </div>
            </div>`;
    }
    string += `</div>`;
    return string;
    
}

function calculateArraySum(arr) {
    return arr.reduce((acc, value) => acc + value, 0);
}

function calculateArrayAverage(arr) {
    if (arr.length === 0) {
        return 0; 
    }
    const sum = calculateArraySum(arr);
    const average = sum / arr.length;
    return average;
}

function formatNumberAbbreviation(number) {
    const SI_SYMBOL = ["", "k", "M", "G", "T", "P", "E"];

    // Find the appropriate SI symbol
    const tier = Math.log10(Math.abs(number)) / 3 | 0;

    // If the tier is less than or equal to 0, return the original number
    if (tier <= 0) return number;

    // Calculate the suffix and format the number
    const suffix = SI_SYMBOL[tier];
    const scale = Math.pow(10, tier * 3);
    const scaledNumber = number / scale;

    // Format the number to have at most 2 decimal places
    const formattedNumber = scaledNumber.toFixed(2);

    return formattedNumber + suffix;
}