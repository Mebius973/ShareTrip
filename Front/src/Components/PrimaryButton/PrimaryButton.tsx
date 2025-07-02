import './PrimaryButton.css'

type PrimaryButtonProps = {
    text: string;
    action: () => void
};

function PrimaryButton(props: PrimaryButtonProps) {
    return (
        <>
            <button className='primary-button main-button' onClick={props.action}>
                {props.text}
            </button>
        </>
    )
}

export default PrimaryButton