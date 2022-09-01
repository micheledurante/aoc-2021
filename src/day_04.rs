use regex::Regex;

/// A bingo board side length
const BOARD_SIDE: usize = 5;
/// Once flattened, the length of a board's rows and cols
// const BOARD_LENGTH: usize = BOARD_SIDE.pow(BOARD_SIDE as u32) * 2;

/// A contiguous vector of properly ordered rows + cols for each board.
/// e.g. `a11`,`a12`,`a13`,`a14`,`a15`,`a21`,`a22`,`a23`,`a25`,`a25`...
fn create_boards(lines: Vec<&str>) -> Vec<i32> {
    let mut boards = vec![];
    let mut tmp_board: Vec<i32> = vec![];

    for i in 0..lines.len() {
        let numbers: Vec<i32> = Regex::new(r"\W+")
            .unwrap()
            .split(lines[i])
            .filter(|x| !x.is_empty())
            .map(|num| num.parse::<i32>().unwrap())
            .collect();

        // all rows
        for num in numbers {
            tmp_board.push(num)
        }

        if (i + 1) % BOARD_SIDE == 0 {
            // end of the current board
            // cols
            for x in 0..BOARD_SIDE {
                for y in 0..BOARD_SIDE {
                    // traverse cols in the flattened vector by calculating the required jump to
                    // reach the next col position
                    tmp_board.push(tmp_board[x + BOARD_SIDE * y]);
                }
            }
            boards.append(&mut tmp_board); // the appended vec is also emptied here
        }
    }

    boards
}

/// Find the first series of 1s that fills the given board side. This is either a col or a row
/// and return its starting position
fn winning_board(scores: &Vec<i32>, board_side: usize) -> i32 {
    // each BOARD_SIZE elements of the score, apply the bitmap, if successful,
    // calculate winning board no and return its idx
    for i in 0..scores.len() {
        // traverse only on each row or col partition
        if i == 0 || i % board_side == 0 {
            // these are all 1s
            if scores[i..i + board_side].iter().sum::<i32>() == board_side as i32 {
                return i as i32;
            }
        }
    }

    -1
}

fn sum_unmarked_numbers(
    scores: &Vec<i32>,
    boards: &Vec<i32>,
    winner_index: usize,
    board_side: usize,
) -> i32 {
    let mut sum = 0;

    for i in winner_index..(winner_index + (board_side * board_side)) {
        if scores[i] == 0 {
            sum += boards[i]
        }
    }

    sum
}

pub fn part_1(input: &str) -> (i32, i32) {
    let mut lines: Vec<&str> = input.lines().collect();
    let draws: Vec<i32> = lines[0]
        .split(",")
        .filter(|x| !x.is_empty())
        .map(|x| x.parse::<i32>().unwrap())
        .collect();

    // Remember to remove the first line as it contains the draws
    let boards = create_boards(lines.drain(1..).filter(|line| !line.is_empty()).collect());
    // copy the vector to use as a bitmask to determine the winning board
    let mut scores: Vec<i32> = boards.clone().iter().map(|_x| 0).collect();

    // play the bingo!
    let mut winner_index = -1;
    let mut winning_number = 0;
    for draw in draws {
        for i in 0..boards.len() {
            if boards[i] == draw {
                scores[i] = 1;
                winner_index = winning_board(&scores, BOARD_SIDE);

                if winner_index != -1 {
                    winning_number = draw;
                    break;
                }
            }
        }

        if winner_index != -1 {
            break;
        }
    }

    (
        winning_number,
        sum_unmarked_numbers(&scores, &boards, winner_index as usize, BOARD_SIDE),
    )
}

#[cfg(test)]
mod day_04_tests {
    use super::*;
    use crate::AOC_2021_SRC_PATH;
    use std::{env, fs};

    #[test]
    fn part_1_test() {
        let result = part_1(
            fs::read_to_string(env::var(AOC_2021_SRC_PATH).unwrap() + "day_04_test.txt")
                .unwrap()
                .as_str(),
        );
        assert_eq!(188 * 24, result.0 * result.1);
    }

    mod sum_unmarked_numbers_tests {
        use super::super::*;

        const BOARD_SIDE: usize = 5;

        #[test]
        /// Sum the numbers that were not marked as drawn during the bingo.
        fn sum_numbers() {
            let scores = vec![
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1,
                0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1,
                1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0,
                1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0, 1, 1,
            ];
            let boards = vec![
                22, 13, 17, 11, 0, 8, 2, 23, 4, 24, 21, 9, 14, 16, 7, 6, 10, 3, 18, 5, 1, 12, 20,
                15, 19, 22, 8, 21, 6, 1, 13, 2, 9, 10, 12, 17, 23, 14, 3, 20, 11, 4, 16, 18, 15, 0,
                24, 7, 5, 19, 3, 15, 0, 2, 22, 9, 18, 13, 17, 5, 19, 8, 7, 25, 23, 20, 11, 10, 24,
                4, 14, 21, 16, 12, 6, 3, 9, 19, 20, 14, 15, 18, 8, 11, 21, 0, 13, 7, 10, 16, 2, 17,
                25, 24, 12, 22, 5, 23, 4, 6, 14, 21, 17, 24, 4, 10, 16, 15, 9, 19, 18, 8, 23, 26,
                20, 22, 11, 13, 6, 5, 2, 0, 12, 3, 7, 14, 10, 18, 22, 2, 21, 16, 8, 11, 0, 17, 15,
                23, 13, 12, 24, 9, 26, 6, 3, 4, 19, 20, 5, 7,
            ];
            assert_eq!(188, sum_unmarked_numbers(&scores, &boards, 100, BOARD_SIDE))
        }
    }

    mod winning_board_tests {
        use super::super::*;

        const BOARD_SIDE: usize = 3;

        #[test]
        /// No scores on the board
        fn winning_board_single_board_no_winner_empty_scores() {
            let input = vec![0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            assert_eq!(-1, winning_board(&input, BOARD_SIDE))
        }

        #[test]
        /// Scores are not correctly aligned with the board side partitions, i.e. in this test
        /// case, every 3 elements
        fn winning_board_single_board_no_winner_non_contiguous_scores() {
            let input = vec![0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0];
            assert_eq!(-1, winning_board(&input, BOARD_SIDE))
        }

        #[test]
        /// Scores are correctly aligned to a partition (no. 2)
        fn winning_board_single_board_winner() {
            let input = vec![0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0];
            assert_eq!(3, winning_board(&input, BOARD_SIDE))
        }

        #[test]
        /// See winning_board_single_board_no_winner_non_contiguous_scores()
        fn winning_board_multiple_boards_no_winner_non_contiguous_scores() {
            let mut input = vec![0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1];
            input.append(&mut vec![
                0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0,
            ]);
            assert_eq!(-1, winning_board(&input, BOARD_SIDE))
        }

        #[test]
        /// See winning_board_single_board_winner()
        fn winning_board_multiple_boards_winner() {
            let mut input = vec![0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1];
            input.append(&mut vec![
                1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0,
            ]);
            assert_eq!(18, winning_board(&input, BOARD_SIDE))
        }
    }

    mod create_boards_tests {
        use super::super::*;

        #[test]
        /// The flattened board should look like: `row a1..n` + `col a1..n`
        fn flatten_single_board() {
            let input = vec![
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19",
            ];
            let expected = vec![
                22, 13, 17, 11, 0, 8, 2, 23, 4, 24, 21, 9, 14, 16, 7, 6, 10, 3, 18, 5, 1, 12, 20,
                15, 19, 22, 8, 21, 6, 1, 13, 2, 9, 10, 12, 17, 23, 14, 3, 20, 11, 4, 16, 18, 15, 0,
                24, 7, 5, 19,
            ]; // rows + cols
            assert_eq!(expected, create_boards(input))
        }

        #[test]
        /// Flattening multiple boards must retain the order of rows and cols as they appear. This
        /// should be: `row a1..n` + `col a1..n` + `row b1..n` + `col b1..n` allowing to identify
        /// `(BOARD_SIZE * i)` as the starting position of rows and cols for each board.
        fn flatten_multiple_boards() {
            let input = vec![
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19",
                " 3 15  0  2 22",
                " 9 18 13 17  5",
                "19  8  7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
            ];
            let expected = vec![
                22, 13, 17, 11, 0, 8, 2, 23, 4, 24, 21, 9, 14, 16, 7, 6, 10, 3, 18, 5, 1, 12, 20,
                15, 19, 22, 8, 21, 6, 1, 13, 2, 9, 10, 12, 17, 23, 14, 3, 20, 11, 4, 16, 18, 15, 0,
                24, 7, 5, 19, 3, 15, 0, 2, 22, 9, 18, 13, 17, 5, 19, 8, 7, 25, 23, 20, 11, 10, 24,
                4, 14, 21, 16, 12, 6, 3, 9, 19, 20, 14, 15, 18, 8, 11, 21, 0, 13, 7, 10, 16, 2, 17,
                25, 24, 12, 22, 5, 23, 4, 6,
            ]; // rows + cols
            assert_eq!(expected, create_boards(input))
        }
    }
}
