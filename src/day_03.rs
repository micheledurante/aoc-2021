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

fn get_most_common_bits(reports: Vec<&[u8]>, upper: i32) -> Vec<u8> {
    let mut most_common_bits: Vec<u8> = vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    let half = reports.len().div_floor(2) as i32;

    for (i, &u) in get_ones_count(reports).iter().enumerate() {
        if u > half {
            most_common_bits[i] = 1
        } else if u == half {
            if upper == 1 {
                most_common_bits[i] = 1
            }
        }
    }

    most_common_bits
}

fn get_as_u32(bits: Vec<u8>) -> u32 {
    bits.iter()
        .enumerate()
        .map(|(i, _bit)| (bits[bits.len() - 1 - i] as u32) << i)
        .sum()
}

fn flip_bits(bits: Vec<u8>) -> Vec<u8> {
    bits.iter().map(|&x| (!x as u32 & 0b1u32) as u8).collect()
}

fn counts_1s(reports: &Vec<&[u8]>, pos: usize) -> i32 {
    let mut count = 0;

    for r in reports {
        if r[pos] & 0b1u8 == 1 {
            count += 1
        }
    }

    count
}

fn o2_bit_criteria(len: usize, ones: i32) -> u8 {
    if (2 * ones) / len as i32 >= 1 {
        0b1u8
    } else {
        0b0u8
    }
}

fn co2_bit_criteria(len: usize, ones: i32) -> u8 {
    if (2 * ones) / len as i32 >= 1 {
        0b0u8
    } else {
        0b1u8
    }
}

fn reduce_reports(reports: &Vec<&[u8]>) -> (u32, u32) {
    let word_size = reports[0].len();
    let mut o2 = 0;
    let mut co2 = 0;
    let mut o2_reports: Vec<&[u8]> = reports.clone();
    let mut co2_reports: Vec<&[u8]> = reports.clone();

    for i in 0..word_size {
        if o2_reports.len() == 1 {
            break;
        }

        let bit_criteria = o2_bit_criteria(o2_reports.len(), counts_1s(&o2_reports, i));

        o2_reports.retain(|r| r[i] ^ bit_criteria == 48);

        o2 = get_as_u32(
            o2_reports[0]
                .iter()
                .map(|&x| if x as u8 == 49 { 1 } else { 0 })
                .collect(),
        );
    }

    for i in 0..word_size {
        if co2_reports.len() == 1 {
            break;
        }

        let bit_criteria = co2_bit_criteria(co2_reports.len(), counts_1s(&co2_reports, i));

        co2_reports.retain(|r| r[i] ^ bit_criteria == 48);

        co2 = get_as_u32(
            co2_reports[0]
                .iter()
                .map(|&x| if x as u8 == 49 { 1 } else { 0 })
                .collect(),
        );
    }

    (o2, co2)
}

pub fn part_2(input: &str) -> (u32, u32) {
    let mut reports: Vec<&str> = input.lines().collect();
    reports.sort();
    let result = reduce_reports(&reports.iter().map(|x| x.as_bytes()).collect());

    (result.0, result.1)
}

pub fn part_1() {
    let reports = read_puzzle_input("./data/day_03.txt");
    let most_common_bits = get_most_common_bits(reports.iter().map(|x| x.as_bytes()).collect(), 1);
    let gamma = get_as_u32(most_common_bits.to_vec());
    let epsilon = get_as_u32(flip_bits(most_common_bits));

    print_solution(
        3,
        1,
        format!(
            "the power consumption, product of γ ({0}) and ε ({1}) is {2}.",
            gamma,
            epsilon,
            gamma * epsilon
        )
        .as_str(),
    );
}

#[cfg(test)]
mod tests {
    use super::*;
    use std::fs;

    #[test]
    fn part_2_test() {
        let result = part_2(
            fs::read_to_string("./data/day_03_test.txt")
                .unwrap()
                .as_str(),
        );
        assert_eq!(23 * 10, result.0 * result.1);
    }

    mod counts_1s_tests {
        use super::super::*;

        #[test]
        fn count_1s_pos_0() {
            let input: Vec<&[u8]> = vec![
                "00100".as_bytes(),
                "11110".as_bytes(),
                "10110".as_bytes(),
                "10111".as_bytes(),
                "10101".as_bytes(),
                "01111".as_bytes(),
                "00111".as_bytes(),
                "11100".as_bytes(),
                "10000".as_bytes(),
                "11001".as_bytes(),
                "00010".as_bytes(),
                "01010".as_bytes(),
            ];
            assert_eq!(7, counts_1s(&input, 0))
        }

        #[test]
        fn count_1s_pos_4() {
            let input: Vec<&[u8]> = vec![
                "00100".as_bytes(),
                "11110".as_bytes(),
                "10110".as_bytes(),
                "10111".as_bytes(),
                "10101".as_bytes(),
                "01111".as_bytes(),
                "00111".as_bytes(),
                "11100".as_bytes(),
                "10000".as_bytes(),
                "11001".as_bytes(),
                "00010".as_bytes(),
                "01010".as_bytes(),
            ];
            assert_eq!(5, counts_1s(&input, 4))
        }
    }

    mod reduce_reports_tests {
        use super::super::*;

        #[test]
        fn reduce_reports_test() {
            let sorted_input: Vec<&[u8]> = vec![
                "00010".as_bytes(),
                "00100".as_bytes(),
                "00111".as_bytes(),
                "01010".as_bytes(),
                "01111".as_bytes(),
                "10000".as_bytes(),
                "10101".as_bytes(),
                "10110".as_bytes(),
                "10111".as_bytes(),
                "11001".as_bytes(),
                "11100".as_bytes(),
                "11110".as_bytes(),
            ];
            assert_eq!((23, 10), reduce_reports(&sorted_input))
        }
    }

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
            get_most_common_bits(
                vec![
                    b"100100110110",
                    b"101110110110",
                    b"010100010100",
                    b"000101011010"
                ],
                1
            )
        );
        assert_eq!(
            vec![1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0],
            get_most_common_bits(
                vec![
                    b"100100110110",
                    b"101110110110",
                    b"100110101010",
                    b"100100010100"
                ],
                0
            )
        )
    }

    mod get_as_u32_test {
        use super::super::*;

        #[test]
        fn get_as_u32_tests() {
            assert_eq!(0, get_as_u32(vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
            assert_eq!(1, get_as_u32(vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1]));
            assert_eq!(2, get_as_u32(vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0]));
            assert_eq!(3, get_as_u32(vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1]));
        }
    }

    #[test]
    fn flip_bits_tests() {
        assert_eq!(
            vec![1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
            flip_bits(vec![0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1])
        );
    }

    mod o2_bit_criteria_test {
        use super::super::*;

        #[test]
        fn o2_bit_criteria_tests() {
            assert_eq!(0b0u8, o2_bit_criteria(3, 1));
            assert_eq!(0b1u8, o2_bit_criteria(1, 3));
            assert_eq!(0b1u8, o2_bit_criteria(2, 2));
            assert_eq!(0b1u8, o2_bit_criteria(2, 1));
        }
    }
}
