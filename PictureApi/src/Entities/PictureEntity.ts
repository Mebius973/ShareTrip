import { IsNotEmpty} from 'class-validator';

export class PictureEntity {
    @IsNotEmpty()
    filename: string

    @IsNotEmpty()
    filepath: string

    metadata?: Record<string, any>

    constructor(filename: string, filepath: string, metadata?: Record<string, any>) {
        this.filename = filename
        this.filepath = filepath
        this.metadata = metadata
    }
}