#![feature(int_roundings)]

use std::fs::File;
use std::io::{BufRead, BufReader};
use std::{env, fs};

mod day_01;
mod day_02;
mod day_03;
mod day_04;

const AOC_2021_SRC_PATH: &str = "AOC_2021_SRC_PATH";

fn print_solution(day: i32, part: i32, content: &str) -> () {
    println!("Day {}, part {}: {}", day, part, content)
}

fn read_puzzle_input(filename: &str) -> Vec<String> {
    let file = File::open(env::var(AOC_2021_SRC_PATH).unwrap() + filename).unwrap();
    let lines = BufReader::new(file).lines();
    let mut input = vec![];

    for line in lines {
        input.push(line.unwrap())
    }

    input
}

fn main() {
    day_01::part_1();
    day_01::part_2();
    day_02::part_1();
    day_02::part_2();
    day_03::part_1();
    let day_03_part_2 = day_03::part_2(
        fs::read_to_string(env::var(AOC_2021_SRC_PATH).unwrap() + "day_03.txt")
            .unwrap()
            .as_str(),
    );
    print_solution(
        3,
        2,
        format!(
            "the O2 generation rating ({0}) times CO2 scrubber rating ({1}) is {2}.",
            day_03_part_2.0,
            day_03_part_2.1,
            day_03_part_2.0 * day_03_part_2.1
        )
            .as_str(),
    );
    let day_04_part_1 = day_04::part_1(
        fs::read_to_string(env::var(AOC_2021_SRC_PATH).unwrap() + "day_04.txt")
            .unwrap()
            .as_str(),
    );
    print_solution(
        4,
        1,
        format!(
            "the winning number ({0}) times the sum of all unmarked numbers ({1}) is {2}.",
            day_04_part_1.0,
            day_04_part_1.1,
            day_04_part_1.0 * day_04_part_1.1
        )
        .as_str(),
    );
}
