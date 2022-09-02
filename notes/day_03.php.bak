<?php

function get_as_int($bits) {
    return (intval($bits[11]) << 0) | 
        (intval($bits[10]) << 1) |
        (intval($bits[9]) << 2) |
        (intval($bits[8]) << 3) |
        (intval($bits[7]) << 4) |
        intval(($bits[6]) << 5) |
        (intval($bits[5]) << 6) |
        (intval($bits[4]) << 7) |
        (intval($bits[3]) << 8) |
        (intval($bits[2]) << 9) |
        (intval($bits[1]) << 10) |
        (intval($bits[0]) << 11);
}

try {
    echo get_as_int([1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1]) * get_as_int([0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0]) . PHP_EOL;
    $lines = preg_split("/\r\n|\n|\r/", file_get_contents('data/day_03.txt'));
    // sort($lines);
    // file_put_contents('day_03_sorted.txt', implode("\r\n", $lines));
    $ones_count = [509, 522, 483, 508, 525, 511, 482, 488, 488, 514, 517, 525];

    for ($i = 0, $max = 12; $i < $max; $i++) {
        if (count($lines) === 1) {
            break;
        }

        $lines = array_filter($lines, function ($line) use ($i, $ones_count) {
            return intval($line[$i]) === ($ones_count[$i] > 500 ? 1 : 0);
        });

        echo count($lines) . ', ';
    }

    echo PHP_EOL . end($lines) . PHP_EOL; // 110111000110
    $o2 = get_as_int(end($lines));
    $o22 = get_as_int([1,1,0,1,1,1,0,0,0,1,1,0]);
    echo $o2 . PHP_EOL;
    echo $o22 . PHP_EOL;

    $lines = preg_split("/\r\n|\n|\r/", file_get_contents('data/day_03.txt'));

    for ($i = 0, $max = 12; $i < $max; $i++) {
        if (count($lines) === 1) {
            break;
        }

        $lines = array_filter($lines, function ($line) use ($i, $ones_count) {
            return intval($line[$i]) !== ($ones_count[$i] > 500 ? 1 : 0);
        });

        echo count($lines) . ', ';
    }

    echo PHP_EOL . end($lines) . PHP_EOL; // 001000111110
    $co2 = get_as_int(end($lines));
    $co22 = get_as_int([0,0,1,0,0,0,1,1,1,1,1,0]);
    echo $co2 . PHP_EOL;
    echo $co22 . PHP_EOL;
    echo $o2 * $co2 . PHP_EOL;
    echo get_as_int([1,1,0,1,1,1,0,0,0,1,1,0]) * get_as_int([0,0,1,0,0,0,1,1,1,1,1,0]);
} catch (Exception $e) {
    echo $e->getMessage();
}
