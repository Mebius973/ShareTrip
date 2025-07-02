import './SecondaryButton.css'

type SecondaryButtonProps = {
    text: string;
    action: () => void
};

function SecondaryButton(props: SecondaryButtonProps) {
    return (
        <>
            <button className='secondary-button main-button' onClick={props.action}>
                {props.text}
            </button>
        </>
    )
}

export default SecondaryButton