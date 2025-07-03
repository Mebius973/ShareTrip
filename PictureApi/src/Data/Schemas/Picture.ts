import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';

@Schema({ timestamps: true })
export class Picture {
  @Prop({ required: true }) tripId: string;
  @Prop({ required: true }) url: string;
  @Prop([String]) tags: string[];
  @Prop({type: Object}) metadata?: Record<string, any>;

  @Prop({
    type: [{
      author: String,
      text: String,
      date: { type: Date, default: Date.now },
      replies: [{
        author: String,
        text: String,
        date: { type: Date, default: Date.now },
      }]
    }],
    default: [],
  })
  comments: any[];
}

export type PictureDocument = Picture & Document;
export const PictureSchema = SchemaFactory.createForClass(Picture);
