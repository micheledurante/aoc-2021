use crate::{print_solution, read_puzzle_input};

fn get_ones_count(reports: Vec<&[u8]>) -> Vec<i32> {
    let mut ones_count = vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

    for report in reports.iter() {
        for (i, bit) in report.iter().enumerate() {
            // 0 AND 1 = 0, 1 AND 1 = 1
            if bit & 1 != 0 {
                ones_count[i] = ones_count[i] + 1
            }
        }
    }

    ones_count
}

fn get_most_common_bits(reports: Vec<&[u8]>, rounded_half: i32) -> Vec<u8> {
    let mut most_common_bits: Vec<u8> = vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

    for (i, &u) in get_ones_count(reports).iter().enumerate() {
        if u > rounded_half {
            most_common_bits[i] = 1
        }
    }

    most_common_bits
}

fn get_as_u32(bits: &Vec<u8>) -> u32 {
    ((bits[11] as u32) << 0)
        | ((bits[10] as u32) << 1)
        | ((bits[9] as u32) << 2)
        | ((bits[8] as u32) << 3)
        | ((bits[7] as u32) << 4)
        | ((bits[6] as u32) << 5)
        | ((bits[5] as u32) << 6)
        | ((bits[4] as u32) << 7)
        | ((bits[3] as u32) << 8)
        | ((bits[2] as u32) << 9)
        | ((bits[1] as u32) << 10)
        | ((bits[0] as u32) << 11)
}

fn flip_bits(bits: Vec<u8>) -> Vec<u8> {
    bits.iter()
        .map(|&x| (!x as u32 & 0b000000000001u32) as u8)
        .collect()
}

pub fn part_2() {}

pub fn part_1() {
    let reports = read_puzzle_input("day_03.txt");
    let most_common_bits = get_most_common_bits(
        reports.iter().map(|x| x.as_bytes()).collect(),
        (reports.len().div_floor(2)) as i32,
    );
    let gamma = get_as_u32(&most_common_bits);
    let epsilon = get_as_u32(&flip_bits(most_common_bits));

    print_solution(
        3,
        1,
        format!(
            "power consumption, product of γ ({0}) and ε ({1}) is {2}.",
            gamma,
            epsilon,
            gamma * epsilon
        )
        .as_str(),
    );
}

#[cfg(test)]
mod day_01_tests {
    use super::*;

    #[test]
    fn get_ones_count_tests() {
        assert_eq!(
            vec![2, 1, 1, 3, 1, 0, 2, 3, 0, 3, 2, 0],
            get_ones_count(vec![b"100100110110", b"101110110110", b"010100010100"])
        );
    }

    #[test]
    fn get_most_common_bits_tests() {
        assert_eq!(
            vec![1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0],
            get_most_common_bits(vec![b"100100110110", b"101110110110", b"010100010100"], 1)
        )
    }

    #[test]
    fn get_as_u32_tests() {
        assert_eq!(0, get_as_u32(&vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
        assert_eq!(1, get_as_u32(&vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1]));
        assert_eq!(2, get_as_u32(&vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0]));
        assert_eq!(3, get_as_u32(&vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1]));
    }

    #[test]
    fn flip_bits_tests() {
        assert_eq!(
            vec![1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
            flip_bits(vec![0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1])
        );
    }
}
