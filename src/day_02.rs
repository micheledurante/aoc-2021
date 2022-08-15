use crate::{print_solution, read_puzzle_input};

const FORWARD: &str = "forward";
const DOWN: &str = "down";

pub fn part_2() {
    let cmds = read_puzzle_input("day_02.txt");
    let mut fw: i64 = 0;
    let mut de: i64 = 0;
    let mut aim: i64 = 0;

    for i in cmds {
        let parts: Vec<&str> = i.split_whitespace().collect();

        match parts[0] {
            FORWARD => {
                fw = fw + parts[1].parse::<i64>().unwrap();
                de = de + (aim * parts[1].parse::<i64>().unwrap())
            },
            DOWN => aim = aim + parts[1].parse::<i64>().unwrap(),
            _ => aim = aim - parts[1].parse::<i64>().unwrap()
        }
    }

    let p = fw * de;

    print_solution(
        2,
        1,
        format!("The product of the final aim ({aim}), forward ({fw}) and depth ({de}) positions is {p}.").as_str(),
    );
}

pub fn part_1() {
    let cmds = read_puzzle_input("day_02.txt");
    let mut fw: i64 = 0;
    let mut de: i64 = 0;

    for i in cmds {
        let parts: Vec<&str> = i.split_whitespace().collect();

        match parts[0] {
            FORWARD => fw = fw + parts[1].parse::<i64>().unwrap(),
            DOWN => de = de + parts[1].parse::<i64>().unwrap(),
            _ => de = de - parts[1].parse::<i64>().unwrap()
        }
    }

    let p = fw * de;

    print_solution(
        2,
        1,
        format!("The product of the final forward ({fw}) and depth ({de}) positions is {p}.").as_str(),
    );
}
