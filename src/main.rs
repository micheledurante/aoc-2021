#![feature(int_roundings)]

use std::fs::File;
use std::io::{BufRead, BufReader};
use std::{env, fs};

mod day_01;
mod day_02;
mod day_03;
mod day_04;

fn print_solution(day: i32, part: i32, content: &str) -> () {
    println!("Day {}, part {}: {}", day, part, content)
}

fn read_puzzle_input(path: &str) -> Vec<String> {
    BufReader::new(File::open(path).unwrap())
        .lines()
        .map(|x| x.unwrap())
        .collect()
}

fn day_04() {
    let day_04_part_1 = day_04::part_1(fs::read_to_string("./data/day_04.txt").unwrap().as_str());
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

fn day_03() {
    day_03::part_1();
    let day_03_part_2 = day_03::part_2(fs::read_to_string("./data/day_03.txt").unwrap().as_str());
    print_solution(
        3,
        2,
        format!(
            "the product of the O2 generation rating ({0}) and CO2 scrubber rating ({1}) is {2}.",
            day_03_part_2.0,
            day_03_part_2.1,
            day_03_part_2.0 * day_03_part_2.1
        )
        .as_str(),
    );
}

fn day_02() {
    day_02::part_1();
    day_02::part_2();
}

fn day_01() {
    day_01::part_1();
    day_01::part_2();
}

fn all() {
    day_01();
    day_02();
    day_03();
    day_04();
}

fn main() {
    let args = env::args().collect::<Vec<String>>();

    if args.len() <= 1 {
        return all()
    }

    match args[1].parse::<i32>() {
        Ok(x) => match x {
            1 => day_01(),
            2 => day_02(),
            3 => day_03(),
            4 => day_04(),
            _ => println!("Invalid day: {}", x),
        },
        Err(_) => println!("Invalid input"),
    }
}
