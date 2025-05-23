export interface ReviewDto {
  requestId: string;
  rating: number;
  comment: string;
}

export interface Review {
  rating: number;
  comment: string;
  date: string;
}
