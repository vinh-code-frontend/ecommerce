import { Request, Response } from "express";

export const getHello = (req: Request, res: Response) => {
  res.json({
    message: "Xin chào! Express + TypeScript + Alias đang hoạt động tốt.",
    timestamp: new Date().toISOString(),
  });
};
