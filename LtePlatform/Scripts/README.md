# 前端脚本定义

## 部分重要即将删除的脚本

可用于前端脚本迁移。

### generateRsrpTaStats

```Javascript
                generateRsrpTaStats: function(stats, rsrpIndex) {
                    var rsrpDivisions = [
                        -110,
                        -105,
                        -100,
                        -95,
                        -90,
                        -85,
                        -80,
                        -75,
                        -70,
                        -65,
                        -60,
                        -44
                    ];
                    rsrpIndex = Math.min(Math.max(0, rsrpIndex), 11);
                    var array = _.map(_.range(11),
                        function(index) {
                            var value = stats['tadv' +
                                appFormatService.prefixInteger(index, 2) +
                                'Rsrp' +
                                appFormatService.prefixInteger(rsrpIndex, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    return {
                        seriesName: rsrpIndex === 0
                            ? 'RSRP<-110dBm'
                            : rsrpDivisions[rsrpIndex - 1] + 'dBm<=RSRP<' + rsrpDivisions[rsrpIndex] + 'dBm',
                        categories: [
                            100,
                            200,
                            300,
                            400,
                            500,
                            600,
                            700,
                            800,
                            900,
                            1000,
                            1200,
                            1400,
                            1600,
                            1800,
                            2000,
                            2400,
                            2800,
                            3200,
                            3600,
                            4000,
                            5000,
                            6000,
                            8000,
                            10000,
                            15000
                        ],
                        values: [
                            0.426693975 * array[0],
                            0.426693975 * array[0],
                            0.14661205 * array[0] + 0.280081925 * array[1],
                            0.426693975 * array[1],
                            0.2932241 * array[1] + 0.133469875 * array[2],
                            0.426693975 * array[2],
                            0.426693975 * array[2],
                            0.013142174 * array[2] + 0.413551801 * array[3],
                            0.426693975 * array[3],
                            0.159754224 * array[3] + 0.133469875 * array[4],
                            0.426693975 * array[4],
                            0.426693975 * array[4],
                            0.013142174 * array[4] + 0.413551801 * array[5],
                            0.426693975 * array[5],
                            0.159754224 * array[5] + 0.133469875 * array[6],
                            0.426693975 * array[6],
                            0.426693975 * array[6],
                            0.013142174 * array[6] + 0.413551801 * array[7],
                            0.426693975 * array[7],
                            0.159754224 * array[7] + 0.266939751 * array[8],
                            0.129363573 * array[8] + 0.058883769 * array[9],
                            0.188247342 * array[9],
                            0.376494684 * array[9],
                            0.376374206 * array[9] + 0.000064 * array[10],
                            0.500032002 * array[10]
                        ]
                    };
                }
```

### generateMrsTaStats

```Javascript
                generateMrsTaStats: function(stats) {
                    var array = _.map(_.range(45),
                        function(index) {
                            var value = stats['tadv_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    return {
                        categories: [
                            100,
                            200,
                            300,
                            400,
                            500,
                            600,
                            700,
                            800,
                            900,
                            1000,
                            1150,
                            1300,
                            1450,
                            1600,
                            1750,
                            1900,
                            2050,
                            2200,
                            2350,
                            2500,
                            2700,
                            2900,
                            3100,
                            3300,
                            3500,
                            3700,
                            3900,
                            4100,
                            4300,
                            4500,
                            4700,
                            4900,
                            5500,
                            6500,
                            8000,
                            9500,
                            11500,
                            15000,
                            20000
                        ],
                        values: [
                            array[0] + 0.280082 * array[1],
                            0.719918 * array[1] + 0.560164 * array[2],
                            0.489836 * array[2] + 0.840246 * array[3],
                            0.159754 * array[3] + array[4] + 0.120328 * array[5],
                            0.879672 * array[5] + 0.40041 * array[6],
                            0.59959 * array[6] + 0.680492 * array[7],
                            0.319508 * array[7] + 0.960593 * array[8],
                            0.039427 * array[8] + array[9] + 0.240655 * array[10],
                            0.759345 * array[11] + 0.520737 * array[12],
                            0.479263 * array[12] + 0.40041 * array[13],
                            0.59959 * array[13] + 0.360471 * array[14],
                            0.639529 * array[14] + 0.320533 * array[15],
                            0.679467 * array[15] + 0.280594 * array[16],
                            0.719406 * array[16] + 0.240655 * array[17],
                            0.759345 * array[17] + 0.200717 * array[18],
                            0.799283 * array[18] + 0.160778 * array[19],
                            0.839222 * array[19] + 0.12084 * array[20],
                            0.87916 * array[20] + 0.080901 * array[21],
                            0.919099 * array[21] + 0.040963 * array[22],
                            0.959037 * array[22] + 0.001024 * array[23],
                            0.998976 * array[23] + 0.281106 * array[24],
                            0.718894 * array[24] + 0.561188 * array[25],
                            0.438812 * array[25] + 0.84127 * array[26],
                            0.15873 * array[26] + array[27] + 0.121352 * array[28],
                            0.878648 * array[28] + 0.401434 * array[29],
                            0.598566 * array[29] + 0.681516 * array[30],
                            0.318484 * array[30] + 0.961598 * array[31],
                            0.038402 * array[31] + array[32] + 0.241679 * array[33],
                            0.758321 * array[33] + 0.521761 * array[34],
                            0.478239 * array[34] + 0.801843 * array[35],
                            0.198157 * array[35] + array[36] + 0.081925 * array[37],
                            0.918075 * array[37] + 0.362007 * array[38],
                            0.637993 * array[38] + 0.400282 * array[39],
                            0.599718 * array[39] + 0.200333 * array[40],
                            0.799667 * array[40] + 0.40041 * array[41],
                            0.59959 * array[41] + 0.600486 * array[42],
                            0.399514 * array[42] + 0.300147 * array[43],
                            0.699853 * array[43] + 0.000192 * array[44],
                            0.999808 * array[44]
                        ]
                    };
                }
```

### generateMrsRsrpStats

```Javascript
                generateMrsRsrpStats: function(stats) {
                    var categories = _.range(-140, -43);
                    var values = _.range(0, 97, 0);
                    var array = _.map(_.range(48),
                        function(index) {
                            var value = stats['rsrP_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    var i;
                    for (i = 2; i < 37; i++) {
                        values[i + 24] = array[i];
                    }
                    for (i = 37; i < 47; i++) {
                        values[2 * i - 13] = values[2 * i - 12] = array[i] / 2;
                    }
                    var tail = array[47];
                    for (i = 0; i < 17; i++) {
                        if (tail > values[80 + i] / 2) {
                            tail -= values[80 + i] / 2 + 1;
                            values[81 + i] = values[80 + i] / 2 + 1;
                        } else {
                            values[81 + i] = tail;
                            break;
                        }
                    }
                    var avg = array[1] / 5;
                    var step = (values[26] - avg) / 3;
                    for (i = 0; i < 5; i++) {
                        values[21 + i] = avg + (i - 2) * step;
                    }
                    var head = values[21];
                    for (i = 0; i < 21; i++) {
                        if (head > values[21 - i] / 2) {
                            head -= values[21 - i] / 2 + 1;
                            values[21 - i - 1] = values[21 - i] / 2 + 1;
                        } else {
                            values[21 - i - 1] = head;
                        }
                    }
                    return {
                        categories: categories,
                        values: values
                    };
                }
```