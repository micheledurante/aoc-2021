#![feature(int_roundings)]

use std::env;
use std::fs::File;
use std::io::{BufRead, BufReader};

mod day_01;

const AOC_2021_SRC_PATH: &str = "AOC_2021_SRC_PATH";

fn print_solution(day: i32, part: i32, content: &str) -> () {
    println!("Day {}, part {}: {}", day, part, content)
}

fn read_puzzle_input(filename: &str) -> Vec<i64> {
    let file = File::open(env::var(AOC_2021_SRC_PATH).unwrap() + filename).unwrap();
    let lines = BufReader::new(file).lines();
    let mut input = vec![];

    for line in lines {
        input.push(line.unwrap().parse::<i64>().unwrap())
    }

    input
}

fn main() {
    day_01::part_1();
    day_01::part_2();
}
