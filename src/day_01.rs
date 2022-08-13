use crate::{print_solution, read_puzzle_input};

fn div_by_three(input: usize) -> usize {
    input.div_floor(3) as usize // Discard the remainder
}

pub fn part_1() -> () {
    let scans = read_puzzle_input("day_01.txt");
    let mut no_of_increases = 0;

    for i in (1..scans.len() - 1).rev() {
        if scans[i] > scans[i - 1] {
            no_of_increases = no_of_increases + 1
        }
    }

    print_solution(
        1,
        1,
        format!("there are {no_of_increases} depth measurement increases in the set.").as_str(),
    );
}

pub fn part_2() -> () {
    let scans = read_puzzle_input("day_01.txt");
    let mut no_of_increases = 0;

    for i in 0..(div_by_three(scans.len()) * 3) - 1 {
        if scans[i] < scans[i + 3] {
            no_of_increases = no_of_increases + 1;
        }
    }

    print_solution(
        1,
        2,
        format!("there are {no_of_increases} of sums of three-measurements in the set.").as_str(),
    );
}

#[cfg(test)]
mod day_01_tests {
    use super::*;

    #[test]
    fn div_by_three_tests() {
        assert_eq!(div_by_three(18), 6);
        assert_eq!(div_by_three(17), 5);
    }
}
